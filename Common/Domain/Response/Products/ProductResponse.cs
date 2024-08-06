using System.ComponentModel.DataAnnotations;

namespace Domain.Response.Products
{
    public class ProductResponse : BaseResponse
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
