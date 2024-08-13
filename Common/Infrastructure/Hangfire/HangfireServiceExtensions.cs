using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Hangfire
{
    public static class HangfireServiceExtensions
    {
        public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UsePostgreSqlStorage(configuration.GetConnectionString("Default")));
            services.AddHangfireServer();
            return services;
        }
    }
}
