using MVBToolsLibrary.Models.ProviderModels;
using System.Text.Json;

namespace MVBToolsLibrary.Repository.Api
{
    public class ScryfallApiPriceRepository : IScryfallApiPriceRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public ScryfallApiPriceRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> Get(string id)
        {
            var url = $"{Routes.ScryfallCards.GET}/{id}";

            var response = _httpClient.CreateClient().GetAsync(url).Result;

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<ScryfallCardModel>(text);

            var price = Decimal.Parse(output.Prices.Price);

            return price;
        }
    }
}
