using Domain.Request.Category;
using Domain.Response.Categories;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : CategoryRequest, IRequest<ProcessResult<CategoryResponse>>
    {
    }
}
