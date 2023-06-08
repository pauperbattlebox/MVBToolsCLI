
namespace MVBToolsLibrary.Repository
{
    public class ScryfallApiRepository : IScryfallApiRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public ScryfallApiRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<Stream> Get(string id)
        {
            var url = $"{Routes.ScryfallCards.GET}/{id}";

            var response = await _httpClient.CreateClient().GetAsync(url);

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
