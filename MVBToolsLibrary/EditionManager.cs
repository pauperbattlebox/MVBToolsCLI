using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using MVBToolsLibrary.Repository;
using MVBToolsLibrary.Scrapers;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class EditionManager : IEditionManager
    {
        
        private readonly IChromeDriverSetup _chromeDriverSetup;
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;

        public EditionManager(IMvbApiEditionRepository mvbApiEditionRepository)
        {
            _mvbApiEditionRepository = mvbApiEditionRepository;
        }

        public EditionManager(IChromeDriverSetup chromeDriverSetup)
        {            
            _chromeDriverSetup = chromeDriverSetup;
        }

        public async Task<IEnumerable<EditionModel>> GetAllEditionsFromDb()
        {
            return await _dbRepository.GetAll();
            
        }

        public async Task AddEditionToDb(int editionId)
        {
            var editionToAdd = GetEditionFromApi(editionId).Result;

            await _dbRepository.Insert(editionToAdd);
        }

        public async Task<MvbEditionModel> GetEditionFromApi(int editionId)
        {
            var stream =  await _mvbApiEditionRepository.Get(editionId);

            var output = JsonSerializer.Deserialize<MvbEditionModel>(stream);

            return output;
        }

        public async Task<string> ScrapeEditionFromWebpage(int id)
        {
            CardsphereCardPage cardPage = new CardsphereCardPage(id, _chromeDriverSetup);

            cardPage.ScrapePage();
            
            var title = cardPage.GetEditionTitle();

            return title;
        }

        public List<MVBCardModel> ScrapeCardsAndPrices(int id)
        {
            CardsphereCardPage cardPage = new CardsphereCardPage(id, _chromeDriverSetup);

            cardPage.ScrapePage();

            var cards = cardPage.GetCardsAndPrices();

            return cards;
        }
    }
}
