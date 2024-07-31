using Application.Contracts.Persistence;
using Domain.Response.Brands;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, ProcessResult<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;
        public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<ProcessResult<BrandResponse>> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<BrandResponse> response = new ProcessResult<BrandResponse>();

            try
            {
                var entitybrand = await brandRepository.GetByIdAsync(request.Id);
                response.Result = mapper.Map<BrandResponse>(entitybrand);


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
