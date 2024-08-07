namespace Infrastructure.Repositories
{
    public class LogUserRepository : RepositoryBase<LogUser>, ILogUserRepository
    {
        public LogUserRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
