using MVBToolsLibrary.Models;
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

        public CardManager(ICardDbRepository<MVBCardModel> cardDbRepository)
        {
            _cardDbRepository = cardDbRepository;
        }

        public async Task<MVBCardModel> GetCardFromDb(int id)
        {
            return await _cardDbRepository.Get(id);
        }
    }
}
