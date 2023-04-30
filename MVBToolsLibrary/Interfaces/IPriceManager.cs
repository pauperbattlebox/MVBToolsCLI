using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Interfaces
{
    public interface IPriceManager
    {
        Task<DbCardModel> GetPriceFromDb(int id);
        Task<decimal> GetPriceFromMvbApi(int id);
        Task<decimal> GetPriceFromScryfallApi(string id);
        Task UpsertCardPriceFromMvbApi(int id);
        Task UpsertCardPriceFromScryfallApi(string scryfallId, int cardsphereId);
    }
}