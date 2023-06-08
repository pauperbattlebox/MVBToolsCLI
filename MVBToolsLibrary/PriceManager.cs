using MVBToolsLibrary.Models.ProviderModels;
using MVBToolsLibrary.Repository;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class PriceManager : IPriceManager
    {
        
        private readonly IScryfallApiRepository _scryfallApiRepository;
        private readonly IMvbApiCardRepository _mvbApiCardRepository;

        public PriceManager(IScryfallApiRepository scryfallApiRepository,
                            IMvbApiCardRepository mvbApiCardRepository)
        {
            _scryfallApiRepository = scryfallApiRepository;
            _mvbApiCardRepository = mvbApiCardRepository;
        }

        public async Task<DbCardModel> GetPriceFromDb(int id)
        {
            return await _priceDbRepository.Get(id);
        }

        public async Task<decimal> GetPriceFromMvbApi(int id)
        {
            var response =  await _mvbApiCardRepository.Get(id);

            var deserializedJson = await JsonSerializer.DeserializeAsync<MvbPriceModel>(response);

            var price = deserializedJson.Price;

            return price;
        }

        public async Task<decimal> GetPriceFromScryfallApi(string id)
        {
            var response = await _scryfallApiRepository.Get(id);

            var deserializedJson = await JsonSerializer.DeserializeAsync<ScryfallPriceModel>(response);

            var price = Decimal.Parse(deserializedJson.Price);

            return price;
        }

        public async Task UpsertCardPriceFromMvbApi(int id)
        {
            var price =  await _mvbApiPriceRepository.Get(id);

            await _priceDbRepository.UpdateCardsphere(id, price);
        }

        public async Task UpsertCardPriceFromScryfallApi(string scryfallId, int cardsphereId)
        {
            var price = await GetPriceFromScryfallApi(scryfallId);

            await _priceDbRepository.UpdateScryfall(scryfallId, cardsphereId, price);
        }
    }
}
