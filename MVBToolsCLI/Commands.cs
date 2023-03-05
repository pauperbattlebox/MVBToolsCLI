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

        public static void AddCardsToDbByEdition(int editionId, SqlCrud sqlConnection)
        {
            EditionCardsModel model = Card.GetCardsFromMVBAPI(editionId);

            Card.AddFilteredCardsToDb(sqlConnection, model);
        }

        public static void RefreshScryfallPriceInDb(string scryfallId, SqlCrud sqlConnection)
        {
            Price.UpdateScryfallPriceInDb(scryfallId, sqlConnection);
        }

        public static void RefreshMVBPriceInDb(int csId, SqlCrud sqlConnection)
        {
            Price.UpdateMVBPriceInDb(csId, sqlConnection);
        }

        public static void GetEditionsFromDb(SqlCrud sqlConnection)
        {
            Edition.ReadAllEditionsFromDb(sqlConnection);
        }

        public static void GetCardFromDb(SqlCrud sqlConnection, int csId)
        {
            Card.ReadCardFromDb(sqlConnection, csId);
        }

        public static void GetCardPriceFromDb(SqlCrud sqlConnection, int csId)
        {
            Card.ReadCardPriceFromDb(sqlConnection, csId);
        }

    }
}
