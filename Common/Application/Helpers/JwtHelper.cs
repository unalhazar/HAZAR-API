using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Helpers
{
    public static class JwtHelper
    {
        public static string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }
    }
}
