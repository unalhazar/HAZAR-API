using Application.Contracts.Persistence;
using Domain;
using Domain.Response.Categories;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ProcessResult<CategoryResponse>>
    {
        private readonly ICategoryRepository brandRepository;
        private readonly IMapper mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }

        public async Task<ProcessResult<CategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            ProcessResult<CategoryResponse> response = new ProcessResult<CategoryResponse>();
            try
            {
                var entity = await brandRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    response.Durum = false;
                    response.Mesaj = "Category not found.";
                    response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }
                entity.DeletedDate = DateTime.UtcNow;
                entity.State = (int)State.Pasif;

                await brandRepository.UpdateAsync(entity);
                response.Result = mapper.Map<CategoryResponse>(entity);

                response.Durum = true;
                response.Mesaj = MesajConstats.SilmeMesaji;
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Durum = false;
                response.Mesaj = $"{MesajConstats.HataMesaji} - {ex.Message}";
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}
