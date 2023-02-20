using Microsoft.Extensions.Configuration;
using System;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Newtonsoft.Json.Linq;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System.Data.SqlClient;
using MVBToolsCLI;


namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sqlConnection = new SqlCrud(Utils.GetConnectionString());

            bool continueProgram = true;

            //commands: add edition to db, add cards to db by edition, add/update mvb price per card, add/update scryfall price per card, get all editions from db, 

            while (continueProgram == true)
            {
                Console.Write("Enter a command (addset, viewsets, addcards, addprice, exit): ");

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

            //string editionId = Console.ReadLine();

            //EditionLogic.AddNewEditionToDb(Int32.Parse(editionId), sqlConnection);

            //CardLogic.AddMultipleCardsToDb(Int32.Parse(editionId), sqlConnection);

            //CardLogic cardLogic = new CardLogic();

            //cardLogic.GetBulkDataURLFromScryfall();

            //Commands.AddNewEditionToDb(881, sqlConnection);

            //Commands.AddCardsToDbByEdition(881, sqlConnection);

            //Commands.RefreshScryfallPriceInDb("a742e23c-1991-4dce-b670-dea92a1cf4ec", sqlConnection);

            Commands.RefreshMVBPriceInDb(30590, sqlConnection);

            

            
        }        
    }
}