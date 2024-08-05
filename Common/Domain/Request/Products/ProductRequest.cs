using Domain.Request.Brands;
using Domain.Request.Category;

namespace Domain.Request.Products
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public CategoryRequest Category { get; set; }
        public long? BrandId { get; set; }
        public BrandRequest? Brand { get; set; }
    }
}
