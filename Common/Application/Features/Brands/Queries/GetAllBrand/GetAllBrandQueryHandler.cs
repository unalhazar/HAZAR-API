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
                using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30), System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                {
                    var brands = await _brandRepository.GetAllAsync();
                    var entities = brands.ToList();
                    response.Result = _mapper.Map<List<BrandResponse>>(entities);
                    Transaction.Complete();
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
