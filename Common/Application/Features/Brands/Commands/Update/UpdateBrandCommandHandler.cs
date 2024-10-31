using Application.Abstraction;
using Application.Base;
using Application.Contracts.Persistence;
using Application.Features.Brands.Responses;
using Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommandHandler(
        IBrandRepository brandRepository,
        IMapper mapper,
        IUserService userService,
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<UpdateBrandCommand, ProcessResult<BrandResponse>>
    {
        public async Task<ProcessResult<BrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<BrandResponse> response = new ProcessResult<BrandResponse>();
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

                    var entity = mapper.Map<Brand>(request);
                    entity.UpdatedDate = DateTime.Now;
                    entity.UpdatedUserId = long.Parse(userId);
                    await brandRepository.UpdateAsync(entity);
                    response.Result = mapper.Map<BrandResponse>(entity);
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
