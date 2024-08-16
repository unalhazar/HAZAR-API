using Application.Abstraction;
using Domain.Response.Users;

namespace Application.Features.Users.Commands.LogoutUser
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, LogoutResponse>
    {
        private readonly IAuthService _authService;

        public LogoutUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LogoutResponse> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LogoutAsync(request.Token);
        }
    }
}
