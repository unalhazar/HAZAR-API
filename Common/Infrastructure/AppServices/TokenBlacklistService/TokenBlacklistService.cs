using Application.Abstraction;

namespace Infrastructure.AppServices.TokenBlacklistService
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly HazarDbContext _dbContext;

        public TokenBlacklistService(HazarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTokenToBlacklist(string token, DateTime expiration)
        {
            var blacklistedToken = new TokenBlacklist
            {
                Token = token,
                ExpirationDate = expiration
            };

            await _dbContext.TokenBlacklists.AddAsync(blacklistedToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsTokenBlacklisted(string token)
        {
            return await _dbContext.TokenBlacklists.AnyAsync(t => t.Token == token && t.ExpirationDate > DateTime.Now);
        }
    }
}
