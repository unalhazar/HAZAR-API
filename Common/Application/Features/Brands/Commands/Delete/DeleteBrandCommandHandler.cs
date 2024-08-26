using Application.Base;
using Application.Contracts.Persistence;
using Application.Features.Brands.Responses;
using Application.Helpers;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.Delete
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, ProcessResult<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;


        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProcessResult<BrandResponse>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<BrandResponse> response = new ProcessResult<BrandResponse>();
            try
            {
                var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userId = JwtHelper.GetUserIdFromToken(token);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in token.");
                }
                var entity = await brandRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    response.Durum = false;
                    response.Mesaj = "Brand not found.";
                    response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }
                entity.DeletedDate = DateTime.UtcNow;
                entity.State = (int)State.Pasif;

                await brandRepository.UpdateAsync(entity);
                response.Result = mapper.Map<BrandResponse>(entity);

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
