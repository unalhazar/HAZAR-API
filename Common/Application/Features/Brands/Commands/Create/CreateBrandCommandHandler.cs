using Application.Contracts.Persistence;
using Application.Helpers;
using Domain;
using Domain.Response.Brands;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ProcessResult<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProcessResult<BrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
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
                entity.State = (int)State.Aktif;
                entity.CreatedDate = DateTime.UtcNow;
                entity.UpdatedUserId = long.Parse(userId);
                await brandRepository.AddAsync(entity);
                response.Result = mapper.Map<BrandResponse>(entity);

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

