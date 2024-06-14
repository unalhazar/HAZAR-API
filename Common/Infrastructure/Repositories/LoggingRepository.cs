namespace Infrastructure.Repositories
{
    public class LoggingRepository : RepositoryBase<Log>, ILoggingRepository
    {
        public LoggingRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
