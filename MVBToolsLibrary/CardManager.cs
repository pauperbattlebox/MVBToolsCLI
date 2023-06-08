
using MVBToolsLibrary.Models.ProviderModels;
using MVBToolsLibrary.Repository;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class CardManager : ICardManager
    {
        private readonly ICardDbRepository<MVBCardModel> _cardDbRepository;
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;
        private readonly IProviderRepository<int> _providerRepository;

        public CardManager(ICardDbRepository<MVBCardModel> cardDbRepository, IMvbApiEditionRepository mvbApiEditionRepository)
        {
            _cardDbRepository = cardDbRepository;
            _mvbApiEditionRepository= mvbApiEditionRepository;
            _providerRepository = new MvbApiCardRepository();
        }

        public async Task<MvbCardModel> GetCardFromDb(int id)
        {
            var response = await _providerRepository.Get(id);

            var deserializedJson = await JsonSerializer.DeserializeAsync<MvbCardModel>(response);

            return deserializedJson;
        }

        public async Task<IEnumerable<MvbCardModel>> GetCardsByEditionCode(string mtgJsonCode)
        {
            return await _cardDbRepository.GetAllById(mtgJsonCode);
        }

        public async Task AddCardsToDbByEditionCode(int editionId)
        {
            var model = await _mvbApiEditionRepository.GetCardsByEdition(editionId);

            var json = JsonSerializer.Deserialize<MvbCardModel>(model);

            var filteredCards = from card in json
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                await _cardDbRepository.Insert(card);
            }
        }
    }
}
