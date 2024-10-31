using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        // Parametresiz constructor
        public Product() { }

        // Parametreli constructor
        public Product(
            string name,
            string description,
            decimal price,
            int stock,
            string imageUrl,
            long categoryId,
            Category category,
            long brandId,
            Brand brand)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            Category = category;
            BrandId = brandId;
            Brand = brand;
        }

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