﻿namespace Hazar.API.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection HazarAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
