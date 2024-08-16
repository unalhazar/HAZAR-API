using Domain.Response.Users;

namespace Application.Features.Users.Commands.LogoutUser
{
    public class LogoutUserCommand : IRequest<LogoutResponse>
    {
        public string Token { get; set; }

        public LogoutUserCommand(string token)
        {
            Token = token;
        }
    }
}
