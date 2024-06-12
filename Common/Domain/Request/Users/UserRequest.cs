namespace Domain.Request.Users
{
    public class UserRequest
    {
        public required string UserName { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
