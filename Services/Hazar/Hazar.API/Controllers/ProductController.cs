using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetByIdProductQuery() { Id = id };
            var result = await _mediator.Send(query);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok();
        }

    }
}
