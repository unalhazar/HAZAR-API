﻿using Application.Abstraction;

namespace Infrastructure.AppServices.ExternalService
{
    public class ExternalService : IExternalService
    {
        private readonly HttpClient _client;

        public ExternalService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ExternalServiceClient");
        }

        public async Task<string> GetDataAsync()
        {
            var response = await _client.GetAsync("api/data");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new HttpRequestException("Error retrieving data from external service");
        }
    }
}