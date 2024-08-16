using Domain.Request.Users;
using Domain.Response.Users;

namespace Application.Features.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public RefreshTokenRequest RefreshTokenRequest { get; set; }

        public RefreshTokenCommand(RefreshTokenRequest refreshTokenRequest)
        {
            RefreshTokenRequest = refreshTokenRequest;
        }
    }
}
