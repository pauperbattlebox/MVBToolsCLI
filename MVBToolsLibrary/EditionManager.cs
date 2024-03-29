﻿using MVBToolsLibrary.Mappers;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary.Scrapers;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

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
            var editionToAdd = await GetEditionFromApi(editionId);

            var output = JsonSerializer.DeserializeAsync<MvbEditionModel>(editionToAdd).Result;

            var convertedEditionModel = ToEditionModel.FromMvbEditionModel(output);

            await _dbRepository.Insert(convertedEditionModel);
        }

        public async Task<Stream> GetEditionFromApi(int editionId)
        {
            return await _mvbApiRepository.Get(editionId);
        }

        public async Task<string> ScrapeEditionFromWebpage(int id)
        {
            CardsphereCardPage cardPage = new CardsphereCardPage(id, _chromeDriverSetup);

            cardPage.ScrapePage();
            
            var title = cardPage.GetEditionTitle();

            return title;
        }

        public List<CardModel> ScrapeCardsAndPrices(int id)
        {
            CardsphereCardPage cardPage = new CardsphereCardPage(id, _chromeDriverSetup);

            cardPage.ScrapePage();

            var cards = cardPage.GetCardsAndPrices();

            return cards;
        }
    }
}
