
namespace Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
