using Application.Base;
using Application.Features.Categories.Responses;

namespace Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery : IRequest<ProcessResult<CategoryResponse>>
    {
        public long Id { get; set; }
    }
}
