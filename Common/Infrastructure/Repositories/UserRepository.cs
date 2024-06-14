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
        public UserRepository(HazarDbContext hazarDbContext, IConfiguration configuration, ILoggingService loggingService, IApplicationUserRepository applicationUserRepository)
        {
            _dbContext = hazarDbContext;
            _configuration = configuration;
            _loggingService = loggingService;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            var getUser = await FindUserByEmail(loginRequest.Email);
            if (getUser == null) return new LoginResponse(false, "User not found", "sorry");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, getUser.Password);
            if (checkPassword)
            {
                _loggingService.Log("User logged in.", operation: Operation.Login.ToString(), loginRequest.Email, logLevel: Domain.LogLevel.Information);
                return new LoginResponse(true, "Login successfully", GenerateJWTToken(getUser));
            }
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email)
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


            await _applicationUserRepository.AddAsync(new ApplicationUser()
            {
                Name = registerUserRequest.Name,
                Email = registerUserRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password),
                CreatedDate = DateTime.Now,
                State = true
            });

            //_dbContext.Users.Add(new ApplicationUser()
            //{
            //    Name = registerUserRequest.Name,
            //    Email = registerUserRequest.Email,
            //    Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password),
            //    CreatedDate = DateTime.Now,
            //    State = true
            //});


            _loggingService.Log("New user registered.", "Register", registerUserRequest.Email, logLevel: Domain.LogLevel.Information);

            return new RegistrationResponse(true, "Registration completed");

        }
    }
}
