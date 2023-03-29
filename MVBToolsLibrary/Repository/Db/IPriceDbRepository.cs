using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public interface IPriceDbRepository
    {
        Task Update(int id, decimal price);
        Task<IEnumerable<DbCardModel>> Get(int id);
    }
}