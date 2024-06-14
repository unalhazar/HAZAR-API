namespace Infrastructure.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
