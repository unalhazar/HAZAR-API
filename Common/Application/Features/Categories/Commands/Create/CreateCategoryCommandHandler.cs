using Application.Contracts.Persistence;
using Domain;
using Domain.Response.Categories;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ProcessResult<CategoryResponse>>
    {
        private readonly ICategoryRepository brandRepository;
        private readonly IMapper mapper;

        public CreateCategoryCommandHandler(ICategoryRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<ProcessResult<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<CategoryResponse> response = new ProcessResult<CategoryResponse>();

            try
            {

                var entity = mapper.Map<Category>(request);
                entity.State = (int)State.Aktif;
                entity.CreatedDate = DateTime.UtcNow;
                await brandRepository.AddAsync(entity);
                response.Result = mapper.Map<CategoryResponse>(entity);

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
