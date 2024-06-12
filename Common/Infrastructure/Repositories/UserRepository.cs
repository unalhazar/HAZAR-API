using Application.Contracts;
using Domain.Request.Users;
using Domain.Response.Users;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly HazarDbContext _dbContext;
        public UserRepository(HazarDbContext hazarDbContext)
        {
            _dbContext = hazarDbContext;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            var getUser = await FindUserByEmail(loginRequest.Email);
            if (getUser == null) return new LoginResponse(false, "User not found", "sorry");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, getUser.Password);
            if (checkPassword)
                return new LoginResponse(true, "Login successfully", GenerateJWTToken(getUser));
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest)
        {
            var getUser = await FindUserByEmail(registerUserRequest.Email);
            if (getUser != null)
                return new RegistrationResponse(false, "User already exist");

            _dbContext.Users.Add(new ApplicationUser()
            {
                Name = registerUserRequest.Name,
                Email = registerUserRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password)
            });

            await _dbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration completed");

        }
    }
}
