using Application.Abstraction;
using Infrastructure.AppServices.Background;
using Infrastructure.AppServices.CacheService;
using Infrastructure.AppServices.LogService.GlobalException;
using Infrastructure.AppServices.LogService.User;
using Infrastructure.AppServices.Notification;
using Infrastructure.AppServices.ProductService;
using Infrastructure.AppServices.TokenBlacklistService;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HazarDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)),
                ServiceLifetime.Scoped);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });


            // Logging service'i ekleyin
            services.AddScoped<ILogUserService, LogUserService>();
            services.AddScoped<IGlobalLoggingService, GlobalLoggingService>();

            //Background Service
            services.AddScoped<INotificationService, NotificationService>();
            services.AddHostedService<PeriodicTaskService>();

            // SignalR
            services.AddSignalR();

            //Cache Service
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IProductService, ProductService>();

            // Repository'i ekleyin
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ILogUserRepository, LogUserRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGlobalLoggingRepository, GlobalLoggingRepository>();
            services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();
            // Diğer servis kayıtları

            return services;
        }
    }
}
