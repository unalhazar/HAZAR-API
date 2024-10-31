using Application.Base;
using Application.Features.Products.Requests;
using Application.Features.Products.Responses;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductRequest, IRequest<ProcessResult<ProductResponse>>
    {
        public CreateProductCommand(string name, string description, decimal price, int stock, string? ımageUrl, long categoryId, long brandId) : base(name, description, price, stock, ımageUrl, categoryId, brandId)
        {
        }
    }
}
