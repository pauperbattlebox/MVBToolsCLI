using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary.Scrapers;
using System.CommandLine;

namespace MVBToolsCLI
{
    public class App
    {   
        private readonly IEditionManager _editionManager;
        private readonly ICardManager _cardManager;
        private readonly IPriceDbRepository _priceDbRepository;
        private readonly IPriceManager _priceManager;
        private readonly IMvbApiPriceRepository _mvbApiPriceRepository;
        private readonly IScryfallApiPriceRepository _scryfallApiPriceRepository;
        private readonly IChromeDriverSetup _chromeDriverSetup;

        public App (
            IEditionManager editionManager,
            ICardManager cardManager,
            IPriceDbRepository priceDbRepository,
            IPriceManager priceManager,
            IMvbApiPriceRepository mvbApiPriceRepository,
            IScryfallApiPriceRepository scryfallApiPriceRepository,
            IChromeDriverSetup chromeDriverSetup)
        {            
            _editionManager = editionManager;
            _cardManager = cardManager;
            _priceDbRepository = priceDbRepository;
            _priceManager = priceManager;
            _mvbApiPriceRepository = mvbApiPriceRepository;
            _scryfallApiPriceRepository = scryfallApiPriceRepository;
            _chromeDriverSetup = chromeDriverSetup;
        }

        public async Task<int> Run(string[] args)
        {   
            var rootCommand = new RootCommand();

            
            //Get editions from db
            var getEditionsCommand = new Command("getAllEditions", "Get all editions from db.");

            getEditionsCommand.SetHandler(boolparam =>
            {
                var rows = _editionManager.GetAllEditionsFromDb().Result;

                foreach(var row in rows)
                {
                    Console.WriteLine($"{row.CsName} - {row.MtgJsonCode} - {row.CsId}");
                }
            });

            rootCommand.AddCommand(getEditionsCommand);


            //Add edition to db
            var editionIdArgument = new Argument<int>(
                name: "editionId",
                description: "Edition ID.");

            var addEditionCommand = new Command("addEdition", "Add edition to db by Cardsphere ID.")
            {
                editionIdArgument
            };

            addEditionCommand.SetHandler((editionId) =>
            {
                _editionManager.AddEditionToDb(editionId);                
                
            }, editionIdArgument);

            rootCommand.AddCommand(addEditionCommand);

            //Scrape webpage for edition.
            var scrapeEditionCommand = new Command("scrapeEditionTitle", "Scrape the webpage for edition title.")
            {
                editionIdArgument
            };

            scrapeEditionCommand.SetHandler((editionId) =>
            {
                var result = _editionManager.ScrapeEditionFromWebpage(editionId.ToString()).Result;

                Console.WriteLine(result);

            }, editionIdArgument);

            rootCommand.AddCommand(scrapeEditionCommand);

            //Scrape webpage for cards list and prices.
            var scrapePricesCommand = new Command("scrapePrices", "Scrape webapge for cards and their prices.")
            {
                editionIdArgument
            };

            scrapePricesCommand.SetHandler((editionId) =>
            {
                var result = _editionManager.ScrapeCardsAndPrices(editionId.ToString());

                foreach (var card in result)
                {
                    Console.WriteLine($"{card.CsId} - {card.Name} - {card.Prices.Price}");
                }
            }, editionIdArgument);

            rootCommand.AddCommand(scrapePricesCommand);

            //Get card from db
            var csCardIdArgument = new Argument<int>(
                name: "cardId",
                description: "Card ID.");

            var getCardCommand = new Command("getCard", "Get card from db by Cardshpere ID.")
            {
                csCardIdArgument
            };

            getCardCommand.SetHandler((cardId) =>
            {
                var response = _cardManager.GetCardFromDb(cardId).Result;

                Console.WriteLine($"{response.Name} - {response.MtgJsonCode}");
                
            }, csCardIdArgument);

            rootCommand.AddCommand(getCardCommand);

            //Get cards by edition
            var editionMtgJsonCodeArgument = new Argument<string>(
                name: "mtgJsonCode",
                description: "MTGJSON Edition Code");

            var getCardsByEditionCommand = new Command("getCardsByEdition", "Get all cards from db by MTGJSON Edition Code.")
            {
                editionMtgJsonCodeArgument
            };

            getCardsByEditionCommand.SetHandler((mtgJsonCode) =>
            {
                var rows = _cardManager.GetCardsByEditionCode(mtgJsonCode).Result;

                foreach( var row in rows)
                {
                    Console.WriteLine($"{row.Name} - { row.MtgJsonCode} - {row.CsId}");
                }
            }, editionMtgJsonCodeArgument);

            rootCommand.AddCommand(getCardsByEditionCommand);

            //Add cards by id
            var addCardsByEdition = new Command("addCardsByEdition", "Add all cards from a given edition to db.")
            {
                editionIdArgument
            };

            addCardsByEdition.SetHandler((id) =>
            {
                _cardManager.AddCardsToDbByEditionCode(id);
                
            }, editionIdArgument);

            rootCommand.AddCommand(addCardsByEdition);

            //Get card price
            var viewPriceCommand = new Command("getPrice", "View a card's prices.")
            {
                csCardIdArgument
            };

            viewPriceCommand.SetHandler((csId) =>
            {
                var response = _priceManager.GetPriceFromDb(csId).Result;

                Console.WriteLine($"{response.Name} - Carsphere: {response.CsPrice} - Scryfall: {response.ScryfallPrice}");
                
            }, csCardIdArgument);

            rootCommand.AddCommand(viewPriceCommand);

            //Add price
            var scryfallCardIdOption = new Option<string>(
                name: "--scryfallCardId",
                description: "Scryfall card ID");
            scryfallCardIdOption.AddAlias("-s");

            var csCardIdOption = new Option<int>(
                name: "--cardsphereId",
                description: "Cardsphere ID");
            csCardIdOption.AddAlias("-c");

            var priceSourceOption = new Option<string>(
                name: "--priceSource",
                description: "Source to add price from").FromAmong("cardsphere", "scryfall");
            priceSourceOption.AddAlias("-p");

            var addPriceCommand = new Command("addPrice", "Add or update card price")
            {
                priceSourceOption,
                csCardIdOption,
                scryfallCardIdOption
            };

            addPriceCommand.SetHandler((source, csId, scryfallId) =>
            {
                if (source == "cardsphere")
                {
                    var price = _mvbApiPriceRepository.Get(csId, RoutesBuilder.BuildUrl).Result;

                    _priceDbRepository.UpdateCardsphere(csId, price);
                    
                }
                if (source == "scryfall")
                {
                    var price = _scryfallApiPriceRepository.Get(scryfallId).Result;

                    _priceDbRepository.UpdateScryfall(scryfallId, csId, price);
                }
            }, priceSourceOption, csCardIdOption, scryfallCardIdOption);

            rootCommand.AddCommand(addPriceCommand);

            return await rootCommand.InvokeAsync(args);            
        }        
    }
}
