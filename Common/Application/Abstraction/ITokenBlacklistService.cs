namespace Application.Abstraction
{
    public interface ITokenBlacklistService
    {
        Task AddTokenToBlacklist(string token, DateTime expiration);
        Task<bool> IsTokenBlacklisted(string token);
    }
}
