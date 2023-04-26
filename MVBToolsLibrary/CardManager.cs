using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public class CardManager : ICardManager
    {
        private readonly ICardDbRepository<MVBCardModel> _cardDbRepository;
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;

        public CardManager(ICardDbRepository<MVBCardModel> cardDbRepository, IMvbApiEditionRepository mvbApiEditionRepository)
        {
            _cardDbRepository = cardDbRepository;
            _mvbApiEditionRepository= mvbApiEditionRepository;
        }

        public async Task<MVBCardModel> GetCardFromDb(int id)
        {
            return await _cardDbRepository.Get(id);
        }

        public async Task<IEnumerable<MVBCardModel>> GetCardsByEditionCode(string mtgJsonCode)
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
