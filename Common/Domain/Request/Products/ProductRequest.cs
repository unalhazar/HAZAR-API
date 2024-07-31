using Domain.Entities;

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
        public Category Category { get; set; }
        public long? BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
