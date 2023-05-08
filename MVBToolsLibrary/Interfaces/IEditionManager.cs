using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Interfaces
{
    public interface IEditionManager
    {
        Task<IEnumerable<EditionModel>> GetAllEditionsFromDb();
        Task AddEditionToDb(int editionId);
        Task<string> ScrapeEditionFromWebpage(int id);
        List<MVBCardModel> ScrapeCardsAndPrices(int id);

    }
}