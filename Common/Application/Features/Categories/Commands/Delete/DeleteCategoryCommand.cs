using Application.Base;
using Application.Features.Categories.Responses;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<ProcessResult<CategoryResponse>>
    {
        public long Id { get; set; }
    }
}
