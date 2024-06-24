namespace Application.Contracts.Persistence
{
    public interface ITokenBlacklistService
    {
        Task AddTokenToBlacklist(string token, DateTime expiration);
        Task<bool> IsTokenBlacklisted(string token);
    }
}
