using Application.Abstraction;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Import;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.SearchQuery.Database;
using Application.Features.Products.Queries.SearchQuery.ElasticSearch;
using Infrastructure.AppServices.EmailService;
using Infrastructure.AppServices.LogService.LoggingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IMediator mediator,
        EmailService emailService,
        IElasticSearchService elasticSearchService,
        ILoggingService loggingService)
        : ControllerBase
    {
        [HttpGet("search-elasticsearch")]
        public async Task<IActionResult> SearchElasticSearch()
        {
            var results = await mediator.Send(new SearchElasticSearchQuery());
            return Ok(results.Result);
        }

        [HttpGet("search-database")]
        public async Task<IActionResult> SearchDatabase()
        {
            var results = await mediator.Send(new SearchDatabaseQuery());
            return Ok(results.Result);
        }

        [HttpGet("search-products")]
        public async Task<IActionResult> SearchProducts(string query)
        {
            await elasticSearchService.SearchProductsAsync(query);
            return Ok("Search completed.");
        }

        [HttpGet("search-advanced-products")]
        public async Task<IActionResult> SearchAdvancedProducts(string query, int minPrice, int maxPrice)
        {
            await elasticSearchService.SearchAdvancedProductsAsync(query, minPrice, maxPrice);
            return Ok("Advanced search completed.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? searchTerm = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllProductsQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await mediator.Send(query);
            loggingService.LogInformation("GetAllProducts method called.");
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("admin-only")]
        public IActionResult AdminOnlyAction()
        {
            return Ok("Bu eylem yalnızca adminlere özeldir.");
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail()
        {
            await emailService.SendEmailAsync("aaa@gmail.com", "Test", "Bu bir test mesajıdır");
            return Ok("Email sent successfully!");
        }


        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportProducts(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            var command = new ImportProductsCommand { FilePath = filePath };
            await mediator.Send(command);
            
            System.IO.File.Delete(filePath);

            return Ok("File imported successfully.");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetByIdProductQuery { Id = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
