namespace Domain.Response.Users
{
    public record LoginResponse(bool Flag, string Message = null!, string AccessToken = null!, string RefreshToken = null);
}
