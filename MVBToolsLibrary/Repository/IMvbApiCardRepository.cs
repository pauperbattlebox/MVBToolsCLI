

namespace MVBToolsLibrary.Repository
{
    public interface IMvbApiCardRepository
    {
        Task<Stream> Get(int id);
    }
}
