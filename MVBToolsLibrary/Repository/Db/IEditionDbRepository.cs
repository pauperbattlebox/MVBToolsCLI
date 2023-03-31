

using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public interface IEditionDbRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Insert(EditionModel entity);
    }
}
