using System.Text;

namespace Infrastructure.OutSourceServices.SOAP
{
    internal class WebserviceXGetWeatherService
    {
        private readonly HttpClient _client;

        public WebserviceXGetWeatherService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetWeatherAsync(string city, string country)
        {
            var soapRequest = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                  <soap:Body>
                                    <GetWeather xmlns=""http://www.webserviceX.NET"">
                                      <CityName>{city}</CityName>
                                      <CountryName>{country}</CountryName>
                                    </GetWeather>
                                  </soap:Body>
                                </soap:Envelope>";

            var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://www.webserviceX.NET/GetWeather");

            var response = await _client.PostAsync("http://www.webservicex.net/globalweather.asmx", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
