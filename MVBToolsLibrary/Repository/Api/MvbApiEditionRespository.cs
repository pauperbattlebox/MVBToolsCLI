using MVBToolsLibrary.Models;
using System.Text.Json;

namespace MVBToolsLibrary.Repository.Api
{
    public class MvbApiEditionRespository : IMvbApiEditionRepository
    {
        private readonly IHttpClientFactory _httpClient;
                
        public MvbApiEditionRespository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EditionModel> Get(int id, Func<int, string, string> buildUrl)
        {
            var uri = new Uri(buildUrl(id, "editionById"));

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<EditionModel>(text);

            return output;
        }

        public async Task<EditionCardsModel> GetCardsByEdition(int id, Func<int, string, string> buildUrl)
        {
            var uri = new Uri(buildUrl(id, "editionById"));

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<EditionCardsModel>(text);

            return output;
        }
    }
}
