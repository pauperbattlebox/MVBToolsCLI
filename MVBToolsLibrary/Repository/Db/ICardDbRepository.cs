using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public interface ICardDbRepository<T> where T : class
    {
        Task<IEnumerable<MVBCardModel>> Get(int id);
        Task<IEnumerable<MVBCardModel>> GetAllById(string mtgJsonCode);
        Task Insert(MVBCardModel entity);
    }
}