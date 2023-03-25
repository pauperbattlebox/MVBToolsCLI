using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Json;
using MVBToolsLibrary.Repository;
using System.CommandLine;

namespace MVBToolsCLI
{
    public class App
    {

        private readonly IEditionDbRepository<EditionModel> _editionDbRepository;
        private readonly ICardDbRepository<MVBCardModel> _cardDbRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public App (IEditionDbRepository<EditionModel> editionDbRepository, ICardDbRepository<MVBCardModel> cardDbRepository, IHttpClientFactory httpClientFactory)
        {
            _editionDbRepository = editionDbRepository;
            _cardDbRepository = cardDbRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> Run(string[] args)
        {            
            SqlCrud sqlConnection = new SqlCrud(GetConnectionString());

            IConsoleWriter consoleWriter = new ConsoleWriter();

            IFileReader fileReader = new FileReader();

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
                Commands.AddNewEditionToDb(editionId, sqlConnection, consoleWriter);
            }, editionIdArgument);

            rootCommand.AddCommand(addEditionCommand);

            //Add all cards from mtgjson file
            var addAllCardsCommand = new Command("addAllCards", "Add all cards to db from mtgjson file.");

            addAllCardsCommand.SetHandler(boolparam =>
            {
                Commands.AddCardsToDbFromJsonFile(sqlConnection, "all_ids.json", consoleWriter, fileReader);
            });

            rootCommand.AddCommand(addAllCardsCommand);

            //Get card
            var csCardIdArgument = new Argument<int>(
                name: "cardId",
                description: "Card ID.");

            var getCardCommand = new Command("getCard", "Get card from db by Cardshpere ID.")
            {
                csCardIdArgument
            };

            getCardCommand.SetHandler((cardId) =>
            {
                Commands.GetCardFromDb(sqlConnection, cardId, consoleWriter);
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
                Commands.AddCardsToDbByEdition(editionId, sqlConnection, consoleWriter);
            }, editionIdArgument);

            rootCommand.AddCommand(addCardsByEdition);

            //Get card price
            var viewPriceCommand = new Command("getPrice", "View a card's prices.")
            {
                csCardIdArgument
            };

            viewPriceCommand.SetHandler((csId) =>
            {
                Commands.GetCardPriceFromDb(sqlConnection, csId, consoleWriter);
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
                    Commands.RefreshMVBPriceInDb(csId, sqlConnection, consoleWriter);
                }
                if (source == "scryfall")
                {
                    Commands.RefreshScryfallPriceInDb(scryfallId, sqlConnection, consoleWriter);
                }
            }, priceSourceOption, csCardIdOption, scryfallCardIdOption);

            rootCommand.AddCommand(addPriceCommand);


            return await rootCommand.InvokeAsync(args);        

            static string GetConnectionString(string connectionStringName = "Default")
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
}
