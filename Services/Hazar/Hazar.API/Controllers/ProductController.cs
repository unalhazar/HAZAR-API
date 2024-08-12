using Application.Abstraction;
using Application.Contracts.Persistence;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Import;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.SearchQuery.Database;
using Application.Features.Products.Queries.SearchQuery.ElasticSearch;
using Infrastructure.AppServices.ElasticSearchService;
using Infrastructure.AppServices.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace Hazar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly EmailService _emailService;
        private readonly ElasticSearchService _elasticSearchService;

        public ProductController(IMediator mediator, IProductService productService, EmailService emailService, ElasticSearchService elasticSearchService)
        {
            _mediator = mediator;
            _productService = productService;
            _emailService = emailService;
            _elasticSearchService = elasticSearchService;
        }



        [HttpGet("search-elasticsearch")]
        public async Task<IActionResult> SearchElasticSearch()
        {
            var results = await _mediator.Send(new SearchElasticSearchQuery());
            return Ok(results.Result);
        }

        [HttpGet("search-database")]
        public async Task<IActionResult> SearchDatabase()
        {
            var results = await _mediator.Send(new SearchDatabaseQuery());
            return Ok(results.Result);
        }

        [HttpGet("search-products")]
        public async Task<IActionResult> SearchProducts(string query)
        {
            await _elasticSearchService.SearchProductsAsync(query);
            return Ok("Search completed.");
        }

        [HttpGet("search-advanced-products")]
        public async Task<IActionResult> SearchAdvancedProducts(string query, int minPrice, int maxPrice)
        {
            await _elasticSearchService.SearchAdvancedProductsAsync(query, minPrice, maxPrice);
            return Ok("Advanced search completed.");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string searchTerm = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllProductsQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail()
        {
            await _emailService.SendEmailAsync("aaa@gmail.com", "Test", "Bu bir test mesajıdır");
            return Ok("Email sent successfully!");
        }


        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportProducts(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Save the file temporarily
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Execute the command
            var command = new ImportProductsCommand { FilePath = filePath };
            await _mediator.Send(command);

            // Optionally delete the file after processing
            System.IO.File.Delete(filePath);

            return Ok("File imported successfully.");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetByIdProductQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
