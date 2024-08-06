using Domain.Response.Categories;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<ProcessResult<CategoryResponse>>
    {
        public long Id { get; set; }
    }
}
