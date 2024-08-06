using Application.Contracts.Persistence;
using Domain.Response.Categories;

namespace Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, ProcessResult<CategoryResponse>>
    {
        private readonly ICategoryRepository brandRepository;
        private readonly IMapper mapper;
        public GetByIdCategoryQueryHandler(ICategoryRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<ProcessResult<CategoryResponse>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<CategoryResponse> response = new ProcessResult<CategoryResponse>();

            try
            {
                var entitybrand = await brandRepository.GetByIdAsync(request.Id);
                response.Result = mapper.Map<CategoryResponse>(entitybrand);


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
