namespace Domain.Response.Categories
{
    public class CategoryResponse : BaseResponse
    {
        public string Name { get; set; }

        public List<Domain.Entities.Product> Products { get; set; } = new List<Domain.Entities.Product>();
    }
}
