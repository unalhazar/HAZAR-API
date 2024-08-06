using Domain.Response.Categories;

namespace Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery : IRequest<ProcessResult<CategoryResponse>>
    {
        public long Id { get; set; }
    }
}
