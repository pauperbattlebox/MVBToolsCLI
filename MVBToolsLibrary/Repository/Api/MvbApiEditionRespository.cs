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

        public async Task<EditionModel> Get(int id)
        {
            var url = $"{Routes.MvbEditions.GET}/{id}";

            var response = await _httpClient.CreateClient().GetAsync(url);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<EditionModel>(text);

            return output;
        }

        public async Task<IEnumerable<MVBCardModel>> GetCardsByEdition(int editionId)
        {
            var url = $"{Routes.MvbEditions.GETBYMTGJSONCODE}/{editionId}";

            var response = _httpClient.CreateClient().GetAsync(url).Result;

            var text = await response.Content.ReadAsStringAsync();
                        
            var output = JsonSerializer.Deserialize<EditionCardsModel>(text);

            return output.Cards;
        }
    }
}
