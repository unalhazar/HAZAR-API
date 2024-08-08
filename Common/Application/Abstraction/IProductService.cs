namespace Application.Abstraction
{
    public interface IProductService
    {
        Task ImportProductsAsync(string filePath);
    }
}
