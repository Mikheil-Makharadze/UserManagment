using JsonPlaceholder.Interfaces;
using Newtonsoft.Json;

namespace JsonPlaceholder.Services
{
    public class HttpBaseService : IHttpBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpBaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<T>> GetItems<T>(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var model = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(model);
        }
    }
}
