using Application.Contracts.Persistence;
using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, ProcessResult<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;
        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<ProcessResult<BrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<BrandResponse> response = new ProcessResult<BrandResponse>();
            try
            {
                using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30), System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = mapper.Map<Brand>(request);
                    await brandRepository.UpdateAsync(entity);
                    response.Result = mapper.Map<BrandResponse>(entity);
                    Transaction.Complete();
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
