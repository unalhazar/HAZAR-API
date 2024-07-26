using Domain.Request.Users;
using Domain.Response.Users;

namespace Application.Contracts
{
    public interface IUser
    {
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserRequest request);
        Task<LogoutResponse> LogoutUserAsync(string token);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
