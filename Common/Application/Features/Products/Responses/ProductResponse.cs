using Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Products.Responses
{
    public class ProductResponse : BaseResponse
    {
        public ProductResponse(long ıd, string name, decimal price, int stock, string ımageUrl, long categoryId, string? categoryName, long brandId, string? brandName)
        {
            Id = ıd;
            Name = name;
            Price = price;
            Stock = stock;
            ImageUrl = ımageUrl;
            CategoryId = categoryId;
            CategoryName = categoryName;
            BrandId = brandId;
            BrandName = brandName;
        }

        public ProductResponse()
        {
            throw new NotImplementedException();
        }

        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public long BrandId { get; set; }
        public string? BrandName { get; set; }
    }
}
