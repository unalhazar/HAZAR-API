using Application.Contracts.Persistence;
using Domain.Entities;
using Domain.Response.Brands;
using Microsoft.Extensions.Logging;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ProcessResult<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateBrandCommandHandler> logger;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, ILogger<CreateBrandCommandHandler> logger)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ProcessResult<BrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<BrandResponse> response = new ProcessResult<BrandResponse>();

            try
            {
                var entity = mapper.Map<Brand>(request);
                await brandRepository.AddAsync(entity);

                response.Result = mapper.Map<BrandResponse>(entity);
                response.Durum = true;
                response.Mesaj = MesajConstats.EklemeMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {

                response.Durum = false;
                response.Mesaj = MesajConstats.HataMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                logger.LogError(ex, $"Exception :  {response.Mesaj} - {ex.Message}");
            }

            return response;


        }
    }

}

