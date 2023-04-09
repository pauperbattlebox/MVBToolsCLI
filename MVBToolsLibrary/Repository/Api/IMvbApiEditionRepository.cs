using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiEditionRepository
    {
        Task<EditionModel> Get(int id, Func<int, string, string> buildUrl);
        Task<EditionCardsModel> GetCardsByEdition(int id, Func<int, string, string> buildUrl);
    }
}
