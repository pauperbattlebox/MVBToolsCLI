using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary
{
    public interface ICardManager
    {
        Task<CardModel> GetCardFromDb(int id);
        Task<IEnumerable<CardModel>> GetCardsByEditionCode(string mtgJsonCode);
        Task AddCardsToDbByEditionCode(int editionId);
    }
}