using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = new Brand();
    }
}
