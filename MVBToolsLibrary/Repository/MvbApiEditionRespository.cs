using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using System.Text.Json;

namespace MVBToolsLibrary.Repository
{
    public class MvbApiEditionRespository : IMvbApiEditionRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiEditionRespository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> Get(int id)
        {
            var url = $"{Routes.MvbEditions.GET}/{id}";

            var response = await _httpClient.CreateClient().GetAsync(url);

            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> GetCardsByEdition(int editionId)
        {
            var url = $"{Routes.MvbEditions.GETBYCSID}/{editionId}";

            var response = _httpClient.CreateClient().GetAsync(url).Result;

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
