
namespace MVBToolsLibrary.Repository
{
    public interface IObjectManagerRepository
    {        
        Task Get(int id);
        Task Upsert(int id);
        Task Delete(int id);
    }
}
