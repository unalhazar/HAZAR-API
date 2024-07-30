using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetByIdBrand;
using AutoMapper;
using Domain.Response.Brands;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public BrandController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ProcessResult<List<BrandResponse>>>> GetAll()
        {
            var query = new GetAllBrandQuery();
            return await mediator.Send(query);

        }

        [HttpGet]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> GetById(long id)
        {
            var query = new GetByIdBrandQuery() { Id = id };
            var result = await mediator.Send(query);
            return result;
        }


        [HttpPost]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> Create([FromBody] CreateBrandCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> Update([FromBody] UpdateBrandCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ProcessResult<BrandResponse>>> Delete(long id)
        {
            var cmd = new DeleteBrandCommand() { Id = id };
            var result = await mediator.Send(cmd);
            return result;
        }
    }
}
