using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public interface IPriceDbRepository
    {
        Task UpdateCardsphere(int id, decimal price);
        Task UpdateScryfall(string id, decimal price);
        Task<IEnumerable<DbCardModel>> Get(int id);

    }
}