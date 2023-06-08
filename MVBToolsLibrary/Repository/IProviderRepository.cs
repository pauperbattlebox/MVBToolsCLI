

namespace MVBToolsLibrary.Repository
{
    public interface IProviderRepository<T>
    {
        Task<Stream> Get(T id);
    }
}
