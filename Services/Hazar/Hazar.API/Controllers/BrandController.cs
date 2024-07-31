using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetByIdBrand;
using Domain.Response.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator mediator;

        public BrandController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ProcessResult<List<BrandResponse>>>> GetAll()
        {
            var query = new GetAllBrandQuery();
            return await mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> GetById(long id)
        {
            var query = new GetByIdBrandQuery { Id = id };
            return await mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> Create([FromBody] CreateBrandCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> Update([FromBody] UpdateBrandCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> Delete(long id)
        {
            var cmd = new DeleteBrandCommand { Id = id };
            var result = await mediator.Send(cmd);
            return result;
        }
    }
}
