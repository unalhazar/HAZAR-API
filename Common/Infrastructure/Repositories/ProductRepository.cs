
namespace Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(HazarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
