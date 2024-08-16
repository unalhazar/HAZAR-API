using Domain.Request.Users;
using Domain.Response.Users;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginResponse>
    {
        public LoginRequest LoginRequest { get; set; }

        public LoginUserCommand(LoginRequest loginRequest)
        {
            LoginRequest = loginRequest;
        }
    }
}
