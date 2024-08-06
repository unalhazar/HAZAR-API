namespace Domain.Response.Categories
{
    public class CategoryResponse : BaseResponse
    {
        public string Name { get; set; }

        public List<Product?> Products { get; set; } = new List<Product?>();
    }
}
