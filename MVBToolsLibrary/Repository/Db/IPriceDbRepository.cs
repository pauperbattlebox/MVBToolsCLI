

using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public interface IPriceDbRepository
    {
        Task UpdateCardsphere(int id, decimal price);
        Task UpdateScryfall(string scryfallId, int csId, decimal price);
        Task<CardModel> Get(int id);

    }
}