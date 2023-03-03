using DataAccessLibrary;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sqlConnection = new SqlCrud(GetConnectionString());

            bool continueProgram = true;

            while (continueProgram == true)
            {
                Console.Write("Enter a command (viewsets, addset, viewcard, viewcardprice, addcard, addcards, addprices, exit): ");

                string option = Console.ReadLine();

                if (option == "addset")
                {
                    Console.WriteLine("What set ID would you like to add: ");

                    string setId = Console.ReadLine();

                    Commands.AddNewEditionToDb(Int32.Parse(setId), sqlConnection);
                }

                if (option == "viewsets")
                {
                    Commands.GetEditionsFromDb(sqlConnection);
                }

                if (option == "viewcard")
                {
                    Console.WriteLine("What card ID would you like to view: ");

                    string csId = Console.ReadLine();

                    Commands.GetCardFromDb(sqlConnection, Int32.Parse(csId));
                }

                if (option == "viewcardprice")
                {
                    Console.WriteLine("What card ID would you like to view a price for: ");

                    string csId = Console.ReadLine();

                    Commands.GetCardPriceFromDb(sqlConnection, Int32.Parse(csId));
                }

                if (option == "addcards")
                {
                    Console.WriteLine("What set ID would you likd to add cards for: ");

                    string setId = Console.ReadLine();

                    Commands.AddCardsToDbByEdition(Int32.Parse(setId), sqlConnection);
                }

                if (option == "addprice")
                {
                    Console.WriteLine("Would you like to add from MVB or Scryfall (mvb, scryfall): ");

                    string api = Console.ReadLine();

                    if (api == "mvb")
                    {
                        Console.WriteLine("Enter the Cardsphere ID: ");

                        string csId = Console.ReadLine();

                        Commands.RefreshMVBPriceInDb(Int32.Parse(csId), sqlConnection);
                    }

                    if (api == "scryfall")
                    {
                        Console.WriteLine("Enter the Scryfall ID: ");

                        string scryfallId = Console.ReadLine();

                        Commands.RefreshScryfallPriceInDb(scryfallId, sqlConnection);
                    }
                }

                if (option == "exit")
                {
                    continueProgram = false;
                }
            }

            Console.WriteLine("That's the end");

            Console.ReadLine();

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