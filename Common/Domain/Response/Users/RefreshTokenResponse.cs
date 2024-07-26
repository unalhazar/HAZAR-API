namespace Domain.Response.Users
{
    public class RefreshTokenResponse
    {
        public bool Success { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

        public RefreshTokenResponse(bool success, string jwtToken = null, string refreshToken = null, string message = null)
        {
            Success = success;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            Message = message;
        }
    }
}
