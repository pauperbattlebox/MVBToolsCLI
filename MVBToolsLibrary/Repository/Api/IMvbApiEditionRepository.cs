using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiEditionRepository
    {
        Task<EditionModel> Get(int id);
        Task<IEnumerable<MVBCardModel>> GetCardsByEdition(int editionId);
    }
}
