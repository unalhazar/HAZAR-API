namespace Domain.Response.Users
{
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!);
}
