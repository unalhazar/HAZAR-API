using Application.Features.Users.Requests;
using Application.Features.Users.Responses;

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
