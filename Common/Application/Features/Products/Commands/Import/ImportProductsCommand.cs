namespace Application.Features.Products.Commands.Import
{
    public class ImportProductsCommand : IRequest<Unit>
    {
        public string FilePath { get; set; }
    }
}
