namespace Application.Features.Users.Responses
{
    public class LogoutResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public LogoutResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
