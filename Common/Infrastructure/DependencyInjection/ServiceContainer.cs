using Application.Abstraction;
using Infrastructure.AppServices.Background;
using Infrastructure.AppServices.CacheService;
using Infrastructure.AppServices.ElasticSearchService;
using Infrastructure.AppServices.EmailService;
using Infrastructure.AppServices.ExternalService;
using Infrastructure.AppServices.LogService.GlobalException;
using Infrastructure.AppServices.LogService.LoggingService;
using Infrastructure.AppServices.LogService.User;
using Infrastructure.AppServices.Notification;
using Infrastructure.AppServices.ProductService;
using Infrastructure.AppServices.UserService;
using Infrastructure.Configurations;
using Infrastructure.ElasticSearch;
using Infrastructure.Hangfire;
using Infrastructure.OutSourceServices.GraphQL;
using Infrastructure.OutSourceServices.REST;
using Infrastructure.OutSourceServices.SOAP;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using StackExchange.Redis;
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
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "http://localhost:5116", // HAZAR-AUTH-API'deki Issuer   
                       ValidAudience = "http://localhost:5116", // HAZAR-AUTH-API'deki Audience
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uıcjqQc341keqwe235rjchsTscmnbrjbzeqweqweqe3ft11fqw")) // HAZAR-AUTH-API'deki Key
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole("Admin"));
            });

            // AuthService ile Polly Dayanıklılık Politikaları
            services.AddHttpClient<IAuthService, Infrastructure.AuthService.AuthService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5090");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10))) // Zaman aşımı politikası
            .AddPolicyHandler(Policy<HttpResponseMessage>
            .Handle<Exception>() // Genel bir hata oluşursa
            .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("Geçici olarak hizmet kullanılamıyor. Lütfen daha sonra tekrar deneyin.")
            }));

            // OutSource Service
            services.AddHttpClient<JsonPlaceHolderGetUserService>();
            services.AddHttpClient<SpacexRocketService>();
            services.AddHttpClient<WebserviceXGetWeatherService>();

            // Email Service
            services.AddScoped<EmailService>(sp => new EmailService(
                configuration["Email:SmtpServer"],
                int.Parse(configuration["Email:SmtpPort"]),
                configuration["Email:SmtpUser"],
                configuration["Email:SmtpPass"]
            ));

            // Logging service'i ekleyin
            services.AddScoped<ILogUserService, LogUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGlobalLoggingService, GlobalLoggingService>();
            services.AddSingleton<ILoggingService, LoggingService>();

            // Hangfire
            services.AddHangfireServices(configuration);

            // Health Checks ekleyin
            services.AddProjectHealthChecks(configuration);

            // Polly
            services.AddHttpClientsWithPolly();


            services.AddScoped<IExternalService, ExternalService>();

            //Background Service
            services.AddScoped<INotificationService, NotificationService>();
            services.AddHostedService<PeriodicTaskService>();
            services.AddHostedService<ProductElasticIndexerBackgroundService>();

            // ElasticSearch servisleri ekleyin
            services.AddScoped<ProductElasticIndexer>();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();
            // SignalR
            services.AddSignalR();

            // Redis ConnectionMultiplexer ekleyin
            var redisConnectionString = configuration["Redis:ConnectionString"];
            var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton(connectionMultiplexer);

            // Cache Service
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IProductService, ProductService>();

            // Repository'i ekleyin
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ILogUserRepository, LogUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGlobalLoggingRepository, GlobalLoggingRepository>();
            // Diğer servis kayıtları

            return services;
        }

        // Yeniden Deneme (Retry) Politikası
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt =>
                {
                    Console.WriteLine($"Retrying... attempt {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });
        }


        // Devre Kesici (Circuit Breaker) Politikası
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)); // 5 hata sonrası 30 saniyelik devre kesici
        }
    }
}
