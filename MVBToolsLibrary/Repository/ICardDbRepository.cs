using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository
{
    public interface ICardDbRepository<T> where T : class
    {
        Task<MVBCardModel> Get(int id);
        Task<IEnumerable<MVBCardModel>> GetAllById(int editionId);
        Task<MVBCardModel> Insert(MVBCardModel entity);
        Task<MVBCardModel> Update(MVBCardModel entity);
    }
}