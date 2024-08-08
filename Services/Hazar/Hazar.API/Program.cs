using Application.DependencyInjection;
using Hazar.API.DependencyInjection;
using Hazar.API.Middleware;
using Infrastructure.DependencyInjection;
using Infrastructure.SignalR;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);
// EPPlus lisans konteksini ayarlay�n
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Add services to the container.

builder.Services.AddControllers();

// Redis cache'i yap�land�rma
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "Hazar"; // Bu, cache anahtarlar�na �n ek ekler
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


builder.Services.AddHttpContextAccessor();
builder.Services.ApplicationServices();
builder.Services.InfrastructureServices(builder.Configuration);
builder.Services.HazarAPIServices(builder.Configuration);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseMiddleware<TokenBlacklistMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
