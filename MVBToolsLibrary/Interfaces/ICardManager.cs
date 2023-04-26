using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Interfaces
{
    public interface ICardManager
    {
        Task<MVBCardModel> GetCardFromDb(int id);
        Task<IEnumerable<MVBCardModel>> GetCardsByEditionCode(string mtgJsonCode);
        Task AddCardsToDbByEditionCode(int editionId);
    }
}