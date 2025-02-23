using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ChatbotIntegration
{
    public class AwsChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;
        private readonly string _apiKey;

        public AwsChatbotService(HttpClient httpClient, IOptions<AwsChatbotSettings> settings)
        {
            _httpClient = httpClient;
            _endpointUrl = settings.Value.EndpointUrl;
            _apiKey = settings.Value.ApiKey;
        }

        public async Task<string> GetResponseAsync(string message)
        {
            var requestBody = new
            {
                inputs = message,
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Dodanie nagłówka autoryzacji
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync(_endpointUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(responseContent);

                // Pobranie wartości pola "generated_text"
                string extractedText = jsonArray[0]["generated_text"]?.ToString();
                return extractedText;
            }
            else
            {
                // Zwracamy błąd jeśli zapytanie się nie powiedzie
                return $"Błąd API: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}";
            }
        }
    }
}
