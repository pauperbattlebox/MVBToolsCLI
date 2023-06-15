using MVBToolsLibrary;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System.CommandLine;

namespace MVBToolsCLI
{
    public class App
    {   
        private readonly IEditionManager _editionManager;
        private readonly ICardManager _cardManager;
        private readonly IPriceManager _priceManager;

        public App (
            IEditionManager editionManager,
            ICardManager cardManager,
            IPriceManager priceManager)
        {            
            _editionManager = editionManager;
            _cardManager = cardManager;
            _priceManager = priceManager;
        }

        public async Task<int> Run(string[] args)
        {   
            var rootCommand = new RootCommand();

            //ARGS AND OPTIONS
            var scryfallCardIdArgument = new Argument<string>(
                name: "scryfallCardId",
                description: "Scryfall Card ID.");

            var editionIdArgument = new Argument<int>(
                name: "editionId",
                description: "Edition ID.");

            var csCardIdArgument = new Argument<int>(
                name: "cardsphereCardId",
                description: "Cardsphere Card ID.");

            var editionMtgJsonCodeArgument = new Argument<string>(
                name: "mtgJsonCode",
                description: "MTGJSON Edition Code");


            //Get all editions from db
            var getEditionsCommand = new Command("getAllEditions", "Get all editions from db.");

            getEditionsCommand.SetHandler(boolparam =>
            {
                var rows = _editionManager.GetAllEditionsFromDb().Result;

                foreach(var row in rows)
                {
                    Console.WriteLine($"{row.CardsphereName} - {row.MtgJsonCode} - {row.CardsphereId}");
                }
            });

            rootCommand.AddCommand(getEditionsCommand);


            //Add edition to db
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
            var scrapeEditionCommand = new Command("scrapeEditionTitle", "Scrape Cardsphere edition webapge for edition title.")
            {
                editionIdArgument
            };

            scrapeEditionCommand.SetHandler((editionId) =>
            {
                var result = _editionManager.ScrapeEditionFromWebpage(editionId).Result;

                Console.WriteLine(result);

            }, editionIdArgument);

            rootCommand.AddCommand(scrapeEditionCommand);


            //Scrape webpage for cards list and prices.
            //var scrapePricesCommand = new Command("scrapePrices", "Scrape Cardsphere webapge by edition Id for cards and their prices.")
            //{
            //    editionIdArgument
            //};

            //scrapePricesCommand.SetHandler((editionId) =>
            //{
            //    var result = _editionManager.ScrapeCardsAndPrices(editionId);

            //    foreach (var card in result)
            //    {
            //        Console.WriteLine($"{card.CardshereId} - {card.Name} - {card.Price}");
            //    }
            //}, editionIdArgument);

            //rootCommand.AddCommand(scrapePricesCommand);


            //Get card from db            
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
            var getCardsByEditionCommand = new Command("getCardsByEdition", "Get all cards from db by MTGJSON Edition Code.")
            {
                editionMtgJsonCodeArgument
            };

            getCardsByEditionCommand.SetHandler((mtgJsonCode) =>
            {
                var rows = _cardManager.GetCardsByEditionCode(mtgJsonCode).Result;

                foreach( var row in rows)
                {
                    Console.WriteLine($"{row.Name} - { row.MtgJsonCode} - {row.CardsphereId}");
                }
            }, editionMtgJsonCodeArgument);

            rootCommand.AddCommand(getCardsByEditionCommand);


            //Add cards by id
            var addCardsByEdition = new Command("addCardsByEdition", "Add all cards from a given edition ID to db.")
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
                var dbPrices = _priceManager.GetPriceFromDb(csId).Result;
                var currentCsPrice = _priceManager.GetPriceFromMvbApi(csId).Result;

                Console.WriteLine($"DB prices: {dbPrices.Name} - Carsphere: {dbPrices.CardspherePrice} - Scryfall: {dbPrices.ScryfallPrice}");
                Console.WriteLine($"Current API price: {currentCsPrice}");
                
            }, csCardIdArgument);

            rootCommand.AddCommand(viewPriceCommand);

            //Add price
            //var scryfallCardIdOption = new Option<string>(
            //    name: "--scryfallCardId",
            //    description: "Scryfall card ID");
            //scryfallCardIdOption.AddAlias("-s");

            //var csCardIdOption = new Option<int>(
            //    name: "--cardsphereId",
            //    description: "Cardsphere ID");
            //csCardIdOption.AddAlias("-c");            

            //var priceSourceOption = new Option<string>(
            //    name: "--priceSource",
            //    description: "Source to add price from").FromAmong("cardsphere", "scryfall");
            //priceSourceOption.AddAlias("-p");            

            var addPriceCommand = new Command("addPrice", "Add or update card prices in db")
            {
                csCardIdArgument,
                scryfallCardIdArgument                
            };

            addPriceCommand.SetHandler((csId, scryfallId) =>
            {                
                _priceManager.UpsertCardPriceFromMvbApi(csId);                

                _priceManager.UpsertCardPriceFromScryfallApi(scryfallId, csId);

                var card = _priceManager.GetPriceFromDb(csId).Result;

                Console.WriteLine($"Cardsphere Price: {card.CardspherePrice} - Scryfall Price: {card.ScryfallPrice}");                
                
            }, csCardIdArgument, scryfallCardIdArgument);

            rootCommand.AddCommand(addPriceCommand);


            return await rootCommand.InvokeAsync(args);            
        }        
    }
}
