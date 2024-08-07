using Application.Abstraction;

namespace Hazar.API.Middleware
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public TokenBlacklistMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var tokenBlacklistService = scope.ServiceProvider.GetRequiredService<ITokenBlacklistService>();

                    var isBlacklisted = await tokenBlacklistService.IsTokenBlacklisted(token);
                    if (isBlacklisted)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Token blacklistlenmiş");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }

}
