
namespace Infrastructure.Repositories
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
