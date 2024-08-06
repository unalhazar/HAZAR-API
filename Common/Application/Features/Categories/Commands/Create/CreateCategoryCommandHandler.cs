using Application.Contracts.Persistence;
using Application.Helpers;
using Domain;
using Domain.Response.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ProcessResult<CategoryResponse>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<CreateCategoryCommandHandler> logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<CreateCategoryCommandHandler> logger)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<ProcessResult<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<CategoryResponse> response = new ProcessResult<CategoryResponse>();

            try
            {
                var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userId = JwtHelper.GetUserIdFromToken(token);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in token.");
                }

                var entity = mapper.Map<Category>(request);
                entity.State = (int)State.Aktif;
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedUserId = long.Parse(userId); // Kullanıcı ID'sini ekle
                await categoryRepository.AddAsync(entity);

                response.Result = mapper.Map<CategoryResponse>(entity);
                response.Durum = true;
                response.Mesaj = MesajConstats.EklemeMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the category.");
                response.Durum = false;
                response.Mesaj = MesajConstats.HataMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
