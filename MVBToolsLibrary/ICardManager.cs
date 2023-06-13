using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary
{
    public interface ICardManager
    {
        Task<MvbCardModel> GetCardFromDb(int id);
        Task<IEnumerable<MvbCardModel>> GetCardsByEditionCode(string mtgJsonCode);
        Task AddCardsToDbByEditionCode(int editionId);
    }
}