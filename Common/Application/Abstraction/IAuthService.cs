using Domain.Request.Users;
using Domain.Response.Users;

namespace Application.Abstraction
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<RegistrationResponse> RegisterAsync(RegisterUserRequest registerRequest);
        Task<LogoutResponse> LogoutAsync(string token);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
