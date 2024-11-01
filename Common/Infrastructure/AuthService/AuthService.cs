using Application.Abstraction;
using Application.Features.Users.Commands.LogoutUser;
using Application.Features.Users.Requests;
using Application.Features.Users.Responses;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Infrastructure.AuthService
{
    public class AuthService(HttpClient httpClient, IOptions<AuthServiceSettings> settings)
        : IAuthService
    {
        private readonly AuthServiceSettings _settings = settings.Value;

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
                var response = await httpClient.PostAsJsonAsync($"{_settings.BaseUrl}{_settings.LoginEndpoint}", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponse>();
                }
                else
                {
                    return new LoginResponse(false, $"Login işlemi başarısız oldu. Hata Kodu: {response.StatusCode} - {response.ReasonPhrase}", null, null);
                }
            }
            catch (TimeoutRejectedException)
            {
                return new LoginResponse(false, "Hizmet yanıt vermiyor. Lütfen daha sonra tekrar deneyin.", null, null);
            }
            catch (BrokenCircuitException)
            {
                return new LoginResponse(false, "Hizmet şu anda kullanılamıyor. Lütfen bir süre sonra tekrar deneyin.", null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login işlemi sırasında bir hata oluştu: {ex.Message}");
                return new LoginResponse(false, "Login işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.", null, null);
            }
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterUserRequest registerRequest)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{_settings.BaseUrl}{_settings.RegisterEndpoint}", registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<RegistrationResponse>();
                }
                else
                {
                    return new RegistrationResponse(false, $"Kayıt işlemi başarısız oldu. Hata Kodu: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (TimeoutRejectedException)
            {
                return new RegistrationResponse(false, "Hizmet yanıt vermiyor. Lütfen daha sonra tekrar deneyin.");
            }
            catch (BrokenCircuitException)
            {
                return new RegistrationResponse(false, "Hizmet şu anda kullanılamıyor. Lütfen bir süre sonra tekrar deneyin.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kayıt işlemi sırasında bir hata oluştu: {ex.Message}");
                return new RegistrationResponse(false, "Kayıt işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        public async Task<LogoutResponse> LogoutAsync(string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_settings.BaseUrl}{_settings.LogoutEndpoint}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var logoutCommand = new LogoutUserCommand(token);
                request.Content = new StringContent(JsonSerializer.Serialize(logoutCommand), Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LogoutResponse>();
                }
                else
                {
                    return new LogoutResponse(false, $"Çıkış işlemi başarısız oldu. Hata Kodu: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (TimeoutRejectedException)
            {
                return new LogoutResponse(false, "Hizmet yanıt vermiyor. Lütfen daha sonra tekrar deneyin.");
            }
            catch (BrokenCircuitException)
            {
                return new LogoutResponse(false, "Hizmet şu anda kullanılamıyor. Lütfen bir süre sonra tekrar deneyin.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Çıkış işlemi sırasında bir hata oluştu: {ex.Message}");
                return new LogoutResponse(false, "Çıkış işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var response = await httpClient.PostAsJsonAsync($"{_settings.BaseUrl}{_settings.RefreshTokenEndpoint}", refreshTokenRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RefreshTokenResponse>();
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Token yenileme işlemi başarısız oldu. Status Code: {response.StatusCode}, Error: {errorMessage}");
        }
    }
}
