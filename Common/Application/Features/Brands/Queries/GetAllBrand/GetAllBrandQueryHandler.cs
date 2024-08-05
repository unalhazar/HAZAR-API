using Application.Contracts.Persistence;
using Domain.Response.Brands;

namespace Application.Features.Brands.Queries.GetAllBrand
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, ProcessResult<List<BrandResponse>>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GetAllBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult<List<BrandResponse>>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<List<BrandResponse>> response = new();

            try
            {

                var brands = await _brandRepository.GetAllAsync();
                var entities = brands.ToList();
                response.Result = _mapper.Map<List<BrandResponse>>(entities);

                response.Durum = true;
                response.Mesaj = "Listeleme İşlemi başarıyla tamamlandı.";
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
