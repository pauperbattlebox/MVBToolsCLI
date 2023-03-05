using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary;
using System.Text.Json;

namespace MVBToolsCLI
{
    public class Commands
    {        
        public static void AddNewEditionToDb(int editionId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            EditionModel editionToAdd = Edition.GetEditionFromMvb(editionId);

            Edition.AddEditionToDb(sqlConnection, editionToAdd, consoleWriter);
        }

        public static void AddCardsToDbByEdition(int editionId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            EditionCardsModel model = Card.GetCardsFromMVBAPI(editionId);

            Card.AddFilteredCardsToDb(sqlConnection, model, consoleWriter);
        }

        public static void RefreshScryfallPriceInDb(string scryfallId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Price.UpdateScryfallPriceInDb(scryfallId, sqlConnection, consoleWriter);
        }

        public static void RefreshMVBPriceInDb(int csId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Price.UpdateMVBPriceInDb(csId, sqlConnection, consoleWriter);
        }

        public static void GetEditionsFromDb(SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Edition.ReadAllEditionsFromDb(sqlConnection, consoleWriter);
        }

        public static void GetCardFromDb(SqlCrud sqlConnection, int csId, IConsoleWriter consoleWriter)
        {
            Card.ReadCardFromDb(sqlConnection, csId, consoleWriter);
        }

        public static void GetCardPriceFromDb(SqlCrud sqlConnection, int csId, IConsoleWriter consoleWriter)
        {
            Card.ReadCardPriceFromDb(sqlConnection, csId, consoleWriter);
        }

    }
}
