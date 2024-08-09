namespace Infrastructure.OutSourceServices.REST
{
    public class JsonPlaceHolderGetUserService
    {
        private readonly HttpClient _client;

        public JsonPlaceHolderGetUserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetUsersAsync()
        {
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
