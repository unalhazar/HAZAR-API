using Application.Base;
using Application.Features.Categories.Requests;
using Application.Features.Categories.Responses;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : CategoryRequest, IRequest<ProcessResult<CategoryResponse>>
    {
    }
}
