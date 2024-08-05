using Application.Contracts;
using Domain;
using Domain.Request.Users;
using Domain.Response.Users;
using Infrastructure.AppServices.LogService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly HazarDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILoggingService _loggingService;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ITokenBlacklistService _tokenBlacklistService;
        public UserRepository(HazarDbContext hazarDbContext, IConfiguration configuration, ILoggingService loggingService, IApplicationUserRepository applicationUserRepository, ITokenBlacklistService tokenBlacklistService)
        {
            _dbContext = hazarDbContext;
            _configuration = configuration;
            _loggingService = loggingService;
            _applicationUserRepository = applicationUserRepository;
            _tokenBlacklistService = tokenBlacklistService;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            var getUser = await FindUserByEmail(loginRequest.Email);
            if (getUser == null) return new LoginResponse(false, "User not found", "sorry");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, getUser.Password);
            if (checkPassword)
            {
                _loggingService.Log("User logged in.", operation: Operation.Login.ToString(), loginRequest.Email, logLevel: Domain.LogLevel.Information);
                var jwtToken = GenerateJWTToken(getUser);
                var refreshToken = GenerateRefreshToken(getUser);
                return new LoginResponse(true, "Login successful", jwtToken, refreshToken.Token);
            }
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        public async Task<LogoutResponse> LogoutUserAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new LogoutResponse(false, "Token bulunamadı.");
            }

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var expirationDate = jwtToken.ValidTo;

            await _tokenBlacklistService.AddTokenToBlacklist(token, expirationDate);

            return new LogoutResponse(true, "Çıkış başarılı.");
        }

        private RefreshToken GenerateRefreshToken(ApplicationUser user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = (int)user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(30), // Token'in geçerlilik süresi
                IsActive = true
            };
            _dbContext.RefreshTokens.Add(refreshToken);
            _dbContext.SaveChanges();
            return refreshToken;
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.Token && rt.IsActive);
            if (refreshToken == null || refreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                return new RefreshTokenResponse(false, "Invalid or expired refresh token");
            }

            var user = await _dbContext.Users.FindAsync(refreshToken.UserId);
            if (user == null)
            {
                return new RefreshTokenResponse(false, "Invalid refresh token");
            }

            var newJwtToken = GenerateJWTToken(user);
            var newRefreshToken = GenerateRefreshToken(user);

            refreshToken.IsActive = false;
            _dbContext.SaveChanges();

            return new RefreshTokenResponse(true, newJwtToken, newRefreshToken.Token);
        }
        private string GenerateJWTToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest)
        {
            var getUser = await FindUserByEmail(registerUserRequest.Email);
            if (getUser != null)
                return new RegistrationResponse(false, "User already exist");


            var result = await _applicationUserRepository.AddAsync(new ApplicationUser()
            {
                Name = registerUserRequest.Name,
                Email = registerUserRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password),
                CreatedDate = DateTime.UtcNow,
                State = 1,
                Role = UserRoles.User,
            });

            if (result == null)
            {
                _loggingService.Log("New user registered Fail.", "Register", registerUserRequest.Email, logLevel: Domain.LogLevel.Information);
                return new RegistrationResponse(true, "Registration failed");
            }

            _loggingService.Log("New user registered.", "Register", registerUserRequest.Email, logLevel: Domain.LogLevel.Information);
            return new RegistrationResponse(true, "Registration completed");

        }
    }
}
