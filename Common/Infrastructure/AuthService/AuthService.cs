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

namespace Infrastructure.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5090/api/User/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponse>();
                }
                else
                {
                    // Hata durumunda anlamlı bir mesaj içeren LoginResponse döndür
                    return new LoginResponse(false, $"Login işlemi başarısız oldu. Hata Kodu: {response.StatusCode} - {response.ReasonPhrase}", null, null);
                }
            }
            catch (TimeoutRejectedException)
            {
                // Zaman aşımı durumunda anlamlı bir mesaj döndür
                return new LoginResponse(false, "Hizmet yanıt vermiyor. Lütfen daha sonra tekrar deneyin.", null, null);
            }
            catch (BrokenCircuitException)
            {
                // Devre kesici durumu devredeyken anlamlı bir mesaj döndür
                return new LoginResponse(false, "Hizmet şu anda kullanılamıyor. Lütfen bir süre sonra tekrar deneyin.", null, null);
            }
            catch (Exception ex)
            {
                // Diğer beklenmedik hatalar için loglama ve genel hata mesajı döndürme
                Console.WriteLine($"Login işlemi sırasında bir hata oluştu: {ex.Message}");

                return new LoginResponse(false, "Login işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.", null, null);
            }
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterUserRequest registerRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5090/api/User/register", registerRequest);

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
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5090/api/User/logout");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // LogoutUserCommand sınıfına 'token' parametresi ile bir nesne 
                var logoutCommand = new LogoutUserCommand(token);

                // JSON olarak logoutCommand
                request.Content = new StringContent(JsonSerializer.Serialize(logoutCommand), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

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
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5090/api/User/refresh-token", refreshTokenRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RefreshTokenResponse>();
            }

            // Hata durumunda ne yapılacağına dair kod
            return null;
        }
    }
}
