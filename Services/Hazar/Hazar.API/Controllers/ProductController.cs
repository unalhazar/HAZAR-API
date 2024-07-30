using Application.Features.Products.Queries.GetAllProducts;
using MediatR;
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
        public async Task<IActionResult> GetById()
        {
            //var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            //var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update()
        {
            //var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            //var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok();
        }

    }
}
