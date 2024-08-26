using Application.Base;
using Application.Features.Categories.Requests;
using Application.Features.Categories.Responses;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : CategoryRequest, IRequest<ProcessResult<CategoryResponse>>
    {
    }
}
