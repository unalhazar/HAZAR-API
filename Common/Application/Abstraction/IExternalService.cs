namespace Application.Abstraction
{
    public interface IExternalService
    {
        Task<string> GetDataAsync();
    }
}
