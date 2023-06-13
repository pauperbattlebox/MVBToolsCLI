using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;

namespace MVBToolsLibrary
{
    public class PriceManager : IPriceManager
    {
        private readonly IPriceDbRepository _priceDbRepository;
        private readonly IMvbApiPriceRepository _mvbApiPriceRepository;
        private readonly IScryfallApiPriceRepository _scryfallApiPriceRepository;

        public PriceManager(IPriceDbRepository priceDbRepository,
                            IMvbApiPriceRepository mvbApiPriceRepository,
                            IScryfallApiPriceRepository scryfallApiPriceRepository)
        {
            _priceDbRepository = priceDbRepository;
            _mvbApiPriceRepository = mvbApiPriceRepository;
            _scryfallApiPriceRepository = scryfallApiPriceRepository;
        }

        public async Task<CardModel> GetPriceFromDb(int id)
        {
            return await _priceDbRepository.Get(id);
        }

        public async Task<decimal> GetPriceFromMvbApi(int id)
        {
            return await _mvbApiPriceRepository.Get(id);
        }

        public async Task<decimal> GetPriceFromScryfallApi(string id)
        {
            return await _scryfallApiPriceRepository.Get(id);
        }

        public async Task UpsertCardPriceFromMvbApi(int id)
        {

            var price =  await _mvbApiPriceRepository.Get(id);

            await _priceDbRepository.UpdateCardsphere(id, price);
        }

        public async Task UpsertCardPriceFromScryfallApi(string scryfallId, int cardsphereId)
        {
            var price = await _scryfallApiPriceRepository.Get(scryfallId);

            await _priceDbRepository.UpdateScryfall(scryfallId, cardsphereId, price);
        }
    }
}
