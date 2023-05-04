using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary.Scrapers;

namespace MVBToolsLibrary
{
    public class EditionManager : IEditionManager
    {
        private readonly IEditionDbRepository<EditionModel> _dbRepository;
        private readonly IMvbApiEditionRepository _mvbApiRepository;
        private readonly IChromeDriverSetup _chromeDriverSetup;

        public EditionManager(IEditionDbRepository<EditionModel> dbRepository, IMvbApiEditionRepository mvbApiRepository, IChromeDriverSetup chromeDriverSetup)
        {
            _dbRepository = dbRepository;
            _mvbApiRepository= mvbApiRepository;
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

        public async Task<EditionModel> GetEditionFromApi(int editionId)
        {
            return await _mvbApiRepository.Get(editionId);
        }

        public async Task<string> ScrapeEditionFromWebpage(string id)
        {
            CardsphereCardPage cardPage = new CardsphereCardPage(id, _chromeDriverSetup);

            cardPage.ScrapePage();
            
            var title = cardPage.GetEditionTitle();

            return title;
        }

        public List<MVBCardModel> ScrapeCardsAndPrices(string id)
        {
            CardsphereCardPage cardPage = new CardsphereCardPage(id, _chromeDriverSetup);

            cardPage.ScrapePage();

            var cards = cardPage.GetCardsAndPrices();

            return cards;
        }
    }
}
