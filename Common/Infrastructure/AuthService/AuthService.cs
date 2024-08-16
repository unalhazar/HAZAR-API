using Application.Abstraction;
using Domain.Request.Users;
using Domain.Response.Users;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
