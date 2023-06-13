
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using System.Text.Json;

namespace MVBToolsLibrary.Repository.Api
{
    public class MvbApiCardRepository : IMvbApiCardRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiCardRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MvbCardModel> GetCard(int id, Func<int, string, string> buildUrl)
        {
            var uri = new Uri(buildUrl(id, "cardById"));

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MvbCardModel>(text);

            return output;
        }        
    }
}
