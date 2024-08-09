using Newtonsoft.Json.Linq;
using System.Text;

namespace Infrastructure.OutSourceServices.GraphQL
{
    public class SpacexRocketService
    {
        private readonly HttpClient _client;

        public SpacexRocketService(HttpClient client)
        {
            _client = client;
        }

        public async Task<JObject> GetRocketDataAsync()
        {
            var query = @"{
                rockets {
                    id
                    name
                    first_flight
                    cost_per_launch
                }
            }";

            var content = new StringContent($"{{\"query\":\"{query}\"}}", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://api.spacex.land/graphql/", content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseData);
        }
    }
}
