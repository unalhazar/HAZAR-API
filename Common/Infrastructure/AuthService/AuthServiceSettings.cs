namespace Infrastructure.AuthService;

public class AuthServiceSettings
{
    public string BaseUrl { get; set; }
    public string LoginEndpoint { get; set; }
    public string RegisterEndpoint { get; set; }
    public string LogoutEndpoint { get; set; }
    public string RefreshTokenEndpoint { get; set; }
}