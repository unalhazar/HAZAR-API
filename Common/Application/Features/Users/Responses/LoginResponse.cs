namespace Application.Features.Users.Responses
{
    public record LoginResponse(bool Flag, string Message = null!, string AccessToken = null!, string RefreshToken = null);
}
