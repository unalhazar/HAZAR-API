using Application.Features.Users.Requests;
using Application.Features.Users.Responses;

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
