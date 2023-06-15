using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiEditionRepository
    {
        Task<Stream> Get(int id);
        Task<Stream> GetCardsByEdition(int editionId);
    }
}
