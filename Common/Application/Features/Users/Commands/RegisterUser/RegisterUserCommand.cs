using Application.Features.Users.Requests;
using Application.Features.Users.Responses;

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
