using Application.Contracts.Persistence;
using Domain.Response.Categories;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ProcessResult<List<CategoryResponse>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            ProcessResult<List<CategoryResponse>> response = new();

            try
            {

                var categories = await _categoryRepository.GetAllAsync();
                var entities = categories.ToList();
                response.Result = _mapper.Map<List<CategoryResponse>>(entities);

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
