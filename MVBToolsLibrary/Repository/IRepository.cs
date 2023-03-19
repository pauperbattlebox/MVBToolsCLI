using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public interface IRepository<T, U> where T : class
    {
        IEnumerable<T> GetAll();
        Task<U> GetAsync(int id);
        Task<T> Insert(T entity);
        Task Save();
    }
}
