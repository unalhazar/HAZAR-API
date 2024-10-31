using Application.Base;
using Application.Contracts.Persistence;
using Application.Features.Brands.Responses;
using Application.Helpers;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommandHandler(
        IBrandRepository brandRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<CreateBrandCommand, ProcessResult<BrandResponse>>
    {
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        public async Task<ProcessResult<BrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
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
                    entity.State = (int)State.Aktif;
                    entity.CreatedDate = DateTime.Now;
                    entity.CreatedUserId = long.Parse(userId);
                    await brandRepository.AddAsync(entity);
                    response.Result = mapper.Map<BrandResponse>(entity);
                }
                response.Durum = true;
                response.Mesaj = MesajConstats.EklemeMesaji;
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

