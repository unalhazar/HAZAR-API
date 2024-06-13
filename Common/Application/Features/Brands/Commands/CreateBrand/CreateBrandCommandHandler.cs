using Application.Contracts.Persistence;
using Domain.Response.Brands;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public partial class CreateBrandCommand
    {
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandResponse>
        {
            private readonly IBrandRepository brandRepository;

            public CreateBrandCommandHandler(IBrandRepository brandRepository)
            {
                this.brandRepository = brandRepository;
            }

            public Task<BrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}

