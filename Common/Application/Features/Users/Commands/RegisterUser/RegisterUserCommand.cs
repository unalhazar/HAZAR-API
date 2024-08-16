using Domain.Request.Users;
using Domain.Response.Users;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegistrationResponse>
    {
        public RegisterUserRequest RegisterRequest { get; set; }

        public RegisterUserCommand(RegisterUserRequest registerRequest)
        {
            RegisterRequest = registerRequest;
        }
    }
}
