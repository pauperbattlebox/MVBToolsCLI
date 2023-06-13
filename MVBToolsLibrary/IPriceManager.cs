using MVBToolsLibrary.Models;

namespace MVBToolsLibrary
{
    public interface IPriceManager
    {
        Task<CardModel> GetPriceFromDb(int id);
        Task<decimal> GetPriceFromMvbApi(int id);
        Task<decimal> GetPriceFromScryfallApi(string id);
        Task UpsertCardPriceFromMvbApi(int id);
        Task UpsertCardPriceFromScryfallApi(string scryfallId, int cardsphereId);
    }
}