using MVBToolsLibrary.Mappers;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MVBToolsLibrary
{
    public class CardManager : ICardManager
    {
        private readonly ICardDbRepository<CardModel> _cardDbRepository;
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;

        public CardManager(ICardDbRepository<CardModel> cardDbRepository, IMvbApiEditionRepository mvbApiEditionRepository)
        {
            _cardDbRepository = cardDbRepository;
            _mvbApiEditionRepository= mvbApiEditionRepository;
        }

        public async Task<CardModel> GetCardFromDb(int id)
        {
            return await _cardDbRepository.Get(id);
        }

        public async Task<IEnumerable<CardModel>> GetCardsByEditionCode(string mtgJsonCode)
        {
            return await _cardDbRepository.GetAllById(mtgJsonCode);
        }

        public async Task AddCardsToDbByEditionCode(int editionId)
        {
            var stream = await _mvbApiEditionRepository.GetCardsByEdition(editionId);

            var output = JsonSerializer.Deserialize<IEnumerable<MvbCardModel>>(stream);

            var filteredCards = from card in output
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                var cardModel = ToCardModel.FromMvbCardModel(card);
                await _cardDbRepository.Insert(cardModel);
            }
        }
    }
}
