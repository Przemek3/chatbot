using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatbotIntegration
{
    public class AwsChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;
        private readonly string _apiKey;

        public AwsChatbotService(HttpClient httpClient, string endpointUrl, string apiKey)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
            _apiKey = apiKey;
        }

        public async Task<string> GetResponseAsync(string message)
        {
            // Tworzymy payload zgodnie z wymaganiami API
            var requestPayload = new { prompt = message };

            // Serializacja do JSON
            var json = JsonConvert.SerializeObject(requestPayload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Dodajemy nagłówek autoryzacji, jeśli jest wymagany
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            // Wysyłamy zapytanie POST do endpointa API
            var response = await _httpClient.PostAsync(_endpointUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Możesz tutaj dodać logikę przetwarzania odpowiedzi (np. deserializację)
                return responseContent;
            }

            return "Błąd podczas komunikacji z usługą AI";
        }
    }
}
