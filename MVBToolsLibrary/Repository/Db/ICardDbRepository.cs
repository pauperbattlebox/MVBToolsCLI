

using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public interface ICardDbRepository<T> where T : class
    {
        Task<CardModel> Get(int id);
        Task<IEnumerable<CardModel>> GetAllById(string mtgJsonCode);
        Task Insert(CardModel entity);
    }
}