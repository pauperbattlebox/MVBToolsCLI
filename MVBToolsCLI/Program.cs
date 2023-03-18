using DataAccessLibrary;
using Microsoft.Extensions.Configuration;
using MVBToolsLibrary;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Json;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel;
using System.Diagnostics.Tracing;

namespace MVBToolsCLI
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            SqlCrud sqlConnection = new SqlCrud(GetConnectionString());

            IConsoleWriter consoleWriter = new ConsoleWriter();

            IFileReader fileReader = new FileReader();

            var rootCommand = new RootCommand();            

            //Get editions from db
            var getEditionsCommand = new Command("edition", "Get all editions from db.");

            getEditionsCommand.SetHandler(boolparam =>
            {
                Commands.GetEditionsFromDb(sqlConnection,
                    consoleWriter);
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

            //View card
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

            //Add cards by set
            var addCardsByEdition = new Command("addCardsByEdition", "Add all cards from a given edition to db.")
            {
                editionIdArgument
            };

            addCardsByEdition.SetHandler((editionId) =>
            {
                Commands.AddCardsToDbByEdition(editionId, sqlConnection, consoleWriter);
            }, editionIdArgument);

            rootCommand.AddCommand(addCardsByEdition);

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

            //    Console.Write("Enter a command (viewcardprice, addallcards): ");

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