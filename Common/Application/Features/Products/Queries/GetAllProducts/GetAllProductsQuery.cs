using Domain.Response.Product;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ProcessResult<List<ProductResponse>>>
    {
    }
}
