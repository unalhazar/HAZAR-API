using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations
{
    public static class HealthChecksConfiguration
    {
        public static IServiceCollection AddProjectHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("Default"), name: "PostgreSQL")
                .AddRedis(configuration["Redis:ConnectionString"], name: "Redis")
                .AddUrlGroup(new Uri(configuration["ExternalApi:HealthCheckUrl"]), name: "External API");

            return services;
        }
    }
}
