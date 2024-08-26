using Application.Base;
using Application.Features.Categories.Responses;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoryQuery : IRequest<ProcessResult<List<CategoryResponse>>>
    {
    }
}
