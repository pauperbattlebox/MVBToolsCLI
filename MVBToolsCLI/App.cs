using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System.CommandLine;
using System.Data;

namespace MVBToolsCLI
{
    public class App
    {

        private readonly IEditionDbRepository<EditionModel> _editionDbRepository;
        private readonly ICardDbRepository<MVBCardModel> _cardDbRepository;
        private readonly IPriceDbRepository _priceDbRepository;
        private readonly IMvbApiCardRepository _mvbApiCardRepository;
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;
        private readonly IMvbApiPriceRepository _mvbApiPriceRepository;
        private readonly IScryfallApiPriceRepository _scryfallApiPriceRepository;
        

        public App (IEditionDbRepository<EditionModel> editionDbRepository,
            ICardDbRepository<MVBCardModel> cardDbRepository,
            IPriceDbRepository priceDbRepository,
            IMvbApiCardRepository mvbApiCardRepository,
            IMvbApiEditionRepository mvbApiEditionRepository,
            IMvbApiPriceRepository mvbApiPriceRepository,
            IScryfallApiPriceRepository scryfallApiPriceRepository)
        {
            _editionDbRepository = editionDbRepository;
            _cardDbRepository = cardDbRepository;
            _priceDbRepository = priceDbRepository;
            _mvbApiCardRepository = mvbApiCardRepository;
            _mvbApiEditionRepository = mvbApiEditionRepository;
            _mvbApiPriceRepository = mvbApiPriceRepository;
            _scryfallApiPriceRepository= scryfallApiPriceRepository;
        }

        public async Task<int> Run(string[] args)
        {   
            var rootCommand = new RootCommand();

            //Get editions from db
            var getEditionsCommand = new Command("getAllEditions", "Get all editions from db.");

            getEditionsCommand.SetHandler(boolparam =>
            {
                var rows = _editionDbRepository.GetAll().Result;                

                foreach(var row in rows)
                {
                    Console.WriteLine($"{row.CsName} - {row.MtgJsonCode}");
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
                var editionModel = _mvbApiEditionRepository.Get(editionId).Result;                

                _editionDbRepository.Insert(editionModel);
                
            }, editionIdArgument);

            rootCommand.AddCommand(addEditionCommand);

            //Add all cards from mtgjson file
            //var addAllCardsCommand = new Command("addAllCards", "Add all cards to db from mtgjson file.");

            //addAllCardsCommand.SetHandler(boolparam =>
            //{
            //    Commands.AddCardsToDbFromJsonFile(sqlConnection, "all_ids.json");
            //});

            //rootCommand.AddCommand(addAllCardsCommand);

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
                var response = _cardDbRepository.Get(cardId).Result;

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
                var rows = _cardDbRepository.GetAllById(mtgJsonCode).Result;

                foreach( var row in rows)
                {
                    Console.WriteLine($"{row.Name} - { row.MtgJsonCode}");
                }
            }, editionMtgJsonCodeArgument);

            rootCommand.AddCommand(getCardsByEditionCommand);

            //Add cards by edition
            var addCardsByEdition = new Command("addCardsByEdition", "Add all cards from a given edition to db.")
            {
                editionIdArgument
            };

            addCardsByEdition.SetHandler((editionId) =>
            {

                var model = _mvbApiEditionRepository.GetCardsByEdition(editionId).Result;

                var filteredCards = from card in model.Cards
                                    where card.IsFoil == false && card.MtgJsonId != null
                                    select card;

                foreach (var card in filteredCards)
                {
                    _cardDbRepository.Insert(card);
                }
                
            }, editionIdArgument);

            rootCommand.AddCommand(addCardsByEdition);

            //Get card price
            var viewPriceCommand = new Command("getPrice", "View a card's prices.")
            {
                csCardIdArgument
            };

            viewPriceCommand.SetHandler((csId) =>
            {
                var models = _priceDbRepository.Get(csId).Result;

                foreach(var model in models)
                {
                    Console.WriteLine($"{model.Name} - Carsphere: {model.CsPrice} - Scryfall: {model.ScryfallPrice}");
                }
                
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
                    var price = _mvbApiPriceRepository.Get(csId).Result;

                    _priceDbRepository.UpdateCardsphere(csId, price);
                    
                }
                if (source == "scryfall")
                {
                    var price = _scryfallApiPriceRepository.Get(scryfallId).Result;

                    _priceDbRepository.UpdateScryfall(scryfallId, price);
                }
            }, priceSourceOption, csCardIdOption, scryfallCardIdOption);

            rootCommand.AddCommand(addPriceCommand);


            return await rootCommand.InvokeAsync(args);            
        }
        public static string GetConnectionString(string connectionStringName = "Default")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
