using Domain.Response.Categories;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoryQuery : IRequest<ProcessResult<List<CategoryResponse>>>
    {
    }
}
