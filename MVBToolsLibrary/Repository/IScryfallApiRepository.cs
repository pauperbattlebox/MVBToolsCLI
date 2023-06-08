
namespace MVBToolsLibrary.Repository
{
    public interface IScryfallApiRepository
    {
        Task<Stream> Get (string id);
    }
}
