using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
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
        public async Task<decimal> Get(int id)
        {
            var url = $"{Routes.MvbCards.GET}/{id}";

            var response = _httpClient.CreateClient().GetAsync(url).Result;

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MvbCardModel>(text);

            var price = output.Prices.Price;

            return price;
        }
    }
}
