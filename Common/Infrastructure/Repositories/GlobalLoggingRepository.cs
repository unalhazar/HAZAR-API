
namespace Infrastructure.Repositories
{
    public class GlobalLoggingRepository : RepositoryBase<GlobalLog>, IGlobalLoggingRepository
    {
        public GlobalLoggingRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
