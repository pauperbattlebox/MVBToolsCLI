using DataAccessLibrary;


namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sqlConnection = new SqlCrud(Utils.GetConnectionString());

            Commands commands = new Commands();

            bool continueProgram = true;            

            while (continueProgram == true)
            {
                Console.Write("Enter a command (addset, viewsets, addcards, addprice, exit): ");

                string option = Console.ReadLine();

                if (option == "addset")
                {
                    Console.WriteLine("What set ID would you like to add: ");

                    string setId = Console.ReadLine();

                    commands.AddNewEditionToDb(Int32.Parse(setId), sqlConnection);
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
            
        }        
    }
}