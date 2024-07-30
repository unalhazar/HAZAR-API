using Domain.Base;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Response.Product
{
    public class ProductResponse : BaseResponse
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
