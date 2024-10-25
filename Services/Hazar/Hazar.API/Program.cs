using Application.DependencyInjection;
using Application.Jobs;
using Domain.Entities;
using Hangfire;
using Hazar.API.DependencyInjection;
using Hazar.API.Middleware;
using Infrastructure.AuthService;
using Infrastructure.DependencyInjection;
using Infrastructure.Hangfire;
using Infrastructure.SignalR;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
// EPPlus lisans
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Add services to the container.

// Serilog yapýlandýrmasý
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ApplicationName", "Hazar.API")
    .WriteTo.Console() // Console'a yaz
                       //.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Günlük dosyasý ekle
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();


builder.Host.UseSerilog();


builder.Services.AddControllers();

// AuthService ve IAuthService servisini ekleyin
builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5090/");
});


// Redis cache'i yapýlandýrma
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "Hazar"; // Bu, cache anahtarlarýna ön ek
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddHttpContextAccessor();
builder.Services.ApplicationServices();
builder.Services.InfrastructureServices(builder.Configuration);
builder.Services.HazarAPIServices(builder.Configuration);
//Policy Tabanlý Yetkilendirme
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole(UserRoles.User));
});
builder.Services.AddSignalR();

// Hangfire
builder.Services.AddHangfireServices(builder.Configuration);

var app = builder.Build();

// Hangfire Dashboard'u
app.UseHangfireDashboard();

// job
RecurringJob.AddOrUpdate<WeeklyJob>("weekly-job", job => job.Execute(), Cron.Minutely);
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
try
{
    Log.Information("Starting web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
