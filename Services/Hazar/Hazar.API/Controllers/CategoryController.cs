using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<ProcessResult<List<CategoryResponse>>>> GetAll()
        //{
        //    //var query = new Get();
        //    //return await mediator.Send(query);

        //}

        //[HttpGet]
        //public async Task<ActionResult<ProcessResult<BrandResponse>>> GetById(long id)
        //{
        //    var query = new GetByIdBrandQuery() { Id = id };
        //    var result = await mediator.Send(query);
        //    return result;
        //}


        //[HttpPost]
        //public async Task<ActionResult<ProcessResult<BrandResponse>>> Create([FromBody] CreateBrandCommand cmd)
        //{
        //    var result = await mediator.Send(cmd);
        //    return result;
        //}

        //[HttpPost]
        //public async Task<ActionResult<ProcessResult<BrandResponse>>> Update([FromBody] UpdateBrandCommand cmd)
        //{
        //    var result = await mediator.Send(cmd);
        //    return result;
        //}

        //[HttpPost]
        //public async Task<ActionResult<ProcessResult<BrandResponse>>> Delete(long id)
        //{
        //    var cmd = new DeleteBrandCommand() { Id = id };
        //    var result = await mediator.Send(cmd);
        //    return result;
        //}
    }
}
