namespace Infrastructure.AuthService;

public class AuthServiceSettings(
    string baseUrl,
    string loginEndpoint,
    string registerEndpoint,
    string logoutEndpoint,
    string refreshTokenEndpoint)
{
    public string BaseUrl { get; set; } = baseUrl;
    public string LoginEndpoint { get; set; } = loginEndpoint;
    public string RegisterEndpoint { get; set; } = registerEndpoint;
    public string LogoutEndpoint { get; set; } = logoutEndpoint;
    public string RefreshTokenEndpoint { get; set; } = refreshTokenEndpoint;
}