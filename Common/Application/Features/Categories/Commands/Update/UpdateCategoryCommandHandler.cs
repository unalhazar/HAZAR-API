using Application.Base;
using Application.Contracts.Persistence;
using Application.Features.Categories.Responses;
using Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ProcessResult<CategoryResponse>>
    {
        private readonly ICategoryRepository brandRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UpdateCategoryCommandHandler(ICategoryRepository brandRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProcessResult<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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

                    var entity = mapper.Map<Category>(request);
                    entity.UpdatedDate = DateTime.Now;
                    entity.UpdatedUserId = long.Parse(userId);
                    await brandRepository.UpdateAsync(entity);
                    response.Result = mapper.Map<CategoryResponse>(entity);
                }


                response.Durum = true;
                response.Mesaj = MesajConstats.GuncellemeMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                response.Durum = false;
                response.Mesaj = MesajConstats.HataMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
