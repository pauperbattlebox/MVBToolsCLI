using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public interface IRepository<T, U> where T : class
    {
        Task <IEnumerable<T>> GetAll();
        Task <T> Get(int id);
        T Insert(T entity);
        void Save();
    }
}
