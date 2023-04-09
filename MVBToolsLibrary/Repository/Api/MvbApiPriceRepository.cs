using MVBToolsLibrary.Models;
using System.Text.Json;

namespace MVBToolsLibrary.Repository.Api
{
    public class MvbApiPriceRepository : IMvbApiPriceRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiPriceRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> Get(int id, Func<int, string, string> buildUrl)
        {   
            var uri = new Uri(buildUrl(id, "cardById"));

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MVBCardModel>(text);

            var price = output.Prices.Price;

            return price;
        }

    }
}
