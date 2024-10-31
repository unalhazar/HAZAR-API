using Application.Base;
using Application.Contracts.Persistence;
using Application.Features.Categories.Responses;
using Application.Helpers;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ProcessResult<CategoryResponse>>
    {
        private readonly ICategoryRepository brandRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DeleteCategoryCommandHandler(ICategoryRepository brandRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProcessResult<CategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<CategoryResponse> response = new ProcessResult<CategoryResponse>();
            try
            {
                var token = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (token != null)
                {
                    var userId = JwtHelper.GetUserIdFromToken(token);

                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new Exception("User ID not found in token.");
                    }

                    var entity = await brandRepository.GetByIdAsync(request.Id);
                    if (entity == null)
                    {
                        response.Durum = false;
                        response.Mesaj = "Category not found.";
                        response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                        return response;
                    }
                    entity.DeletedDate = DateTime.Now;
                    entity.State = (int)State.Pasif;
                    entity.DeletedUserId = long.Parse(userId);
                    await brandRepository.UpdateAsync(entity);
                    response.Result = mapper.Map<CategoryResponse>(entity);
                }

                response.Durum = true;
                response.Mesaj = MesajConstats.SilmeMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Durum = false;
                response.Mesaj = $"{MesajConstats.HataMesaji} - {ex.Message}";
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}
