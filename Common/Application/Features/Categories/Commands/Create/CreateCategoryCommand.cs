using Domain.Request.Category;
using Domain.Response.Categories;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : CategoryRequest, IRequest<ProcessResult<CategoryResponse>>
    {
    }
}
