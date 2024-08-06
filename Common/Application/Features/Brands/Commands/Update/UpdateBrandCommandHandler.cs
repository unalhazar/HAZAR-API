using Application.Contracts.Persistence;
using Application.Helpers;
using Domain.Response.Brands;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, ProcessResult<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProcessResult<BrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
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
                var entity = mapper.Map<Brand>(request);
                entity.UpdatedDate = DateTime.UtcNow;
                await brandRepository.UpdateAsync(entity);
                response.Result = mapper.Map<BrandResponse>(entity);
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
