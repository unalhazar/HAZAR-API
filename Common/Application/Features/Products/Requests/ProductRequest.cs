using Application.Base;

namespace Application.Features.Products.Requests
{
    public class ProductRequest : BaseRequest
    {
        public ProductRequest(string name, string description, decimal price, int stock, string? ımageUrl, long categoryId, long brandId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = ımageUrl;
            CategoryId = categoryId;
            BrandId = brandId;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
    }
}
