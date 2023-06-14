using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiEditionRepository
    {
        Task<EditionModel> Get(int id);
        Task<IEnumerable<MvbCardModel>> GetCardsByEdition(int editionId);
    }
}
