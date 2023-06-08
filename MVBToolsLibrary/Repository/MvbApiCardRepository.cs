

namespace MVBToolsLibrary.Repository
{
    public class MvbApiCardRepository : IMvbApiCardRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiCardRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;            
        }

        public async Task<Stream> Get(int id)
        {
            var url = $"{Routes.MvbCards.GET}/{id}";

            var response = await _httpClient.CreateClient().GetAsync(url);

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
