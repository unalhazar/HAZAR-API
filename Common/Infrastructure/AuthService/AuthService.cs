using Application.Abstraction;
using Application.Features.Users.Commands.LogoutUser;
using Application.Features.Users.Requests;
using Application.Features.Users.Responses;
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
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5090/api/User/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }

            // Hata durumunda ne yapılacağına dair kod
            return null;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterUserRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5090/api/User/register", registerRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            }

            // Hata durumunda ne yapılacağına dair kod
            return null;
        }

        public async Task<LogoutResponse> LogoutAsync(string token)
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

            // Hata durumunda ne yapılacağına dair kod
            return null;
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
