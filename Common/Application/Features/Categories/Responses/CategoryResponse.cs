using Application.Base;

namespace Application.Features.Categories.Responses
{
    public class CategoryResponse : BaseResponse
    {
        public string Name { get; set; }

        public List<Product?> Products { get; set; } = new List<Product?>();
    }
}