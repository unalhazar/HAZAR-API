using Domain.Request.Users;
using Domain.Response.Users;

namespace Application.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
        Task<LoginResponse> LoginUserAsync(LoginRequest loginRequest);
    }
}
