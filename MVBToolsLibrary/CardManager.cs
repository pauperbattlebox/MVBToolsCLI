using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;

namespace MVBToolsLibrary
{
    public class CardManager : ICardManager
    {
        private readonly ICardDbRepository<MvbCardModel> _cardDbRepository;
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;

        public CardManager(ICardDbRepository<MvbCardModel> cardDbRepository, IMvbApiEditionRepository mvbApiEditionRepository)
        {
            _cardDbRepository = cardDbRepository;
            _mvbApiEditionRepository= mvbApiEditionRepository;
        }

        public async Task<MvbCardModel> GetCardFromDb(int id)
        {
            return await _cardDbRepository.Get(id);
        }

        public async Task<IEnumerable<MvbCardModel>> GetCardsByEditionCode(string mtgJsonCode)
        {
            return await _cardDbRepository.GetAllById(mtgJsonCode);
        }

        public async Task AddCardsToDbByEditionCode(int editionId)
        {
            var model = await _mvbApiEditionRepository.GetCardsByEdition(editionId);

            var filteredCards = from card in model
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                await _cardDbRepository.Insert(card);
            }
        }
    }
}
