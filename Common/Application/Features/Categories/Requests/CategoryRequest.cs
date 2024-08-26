using Application.Base;

namespace Application.Features.Categories.Requests
{
    public class CategoryRequest : BaseRequest
    {
        public required string Name { get; set; }
    }
}
