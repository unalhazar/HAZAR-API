using Application.Abstraction;
using Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.AppServices.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserId()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                return null;

            return JwtHelper.GetUserIdFromToken(token);
        }

    }
}
