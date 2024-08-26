using Application.Base;

namespace Application.Features.Products.Requests
{
    public class ProductRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
    }
}
