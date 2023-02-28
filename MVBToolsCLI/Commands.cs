using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Models.Interfaces;
using MVBToolsLibrary;
using MVBToolsLibrary.Json;
using System.Text.Json;

namespace MVBToolsCLI
{
    public class Commands
    {
        //IJsonHandler _jsonHandler;
        //public Commands(IJsonHandler jsonHandler)
        //{
        //    _jsonHandler = jsonHandler;
        //}        

        public void AddNewEditionToDb(int editionId, SqlCrud sqlConnection)
        {
            string endpoint = Edition.GetMVBEditionEndpoint(editionId);

            string response = Utilities.CallEndpoint(endpoint);

            EditionModel output = JsonSerializer.Deserialize<EditionModel>(response);

            Edition.AddEditionToDb(sqlConnection, output);
        }

        public static void AddCardsToDbByEdition(int editionId, SqlCrud sqlConnection)
        {
            EditionCardsModel model = Card.GetCardsFromMVBAPI(editionId);

            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {                
                Card.AddCardToDb(sqlConnection, card);
            }
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

    }
}
