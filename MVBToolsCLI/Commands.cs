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
        IJsonHandler _jsonHandler;
        public Commands(IJsonHandler jsonHandler)
        {
            _jsonHandler = jsonHandler;
        }        

        public void AddNewEditionToDb(int editionId, SqlCrud sqlConnection)
        {
            string endpoint = EditionLogic.GetMVBEditionEndpoint(editionId);

            string response = Utilities.CallEndpoint(endpoint);

            EditionModel output = JsonSerializer.Deserialize<EditionModel>(response);

            EditionLogic.AddEditionToDb(sqlConnection, (EditionModel)output);
        }

        public static void AddCardsToDbByEdition(int editionId, SqlCrud sqlConnection)
        {
            EditionCardsModel model = CardLogic.GetCardsFromMVBAPI(editionId);

            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {                
                CardLogic.AddCardToDb(sqlConnection, card);
            }
        }

        public static void RefreshScryfallPriceInDb(string scryfallId, SqlCrud sqlConnection)
        {
            PriceLogic.UpdateScryfallPriceInDb(scryfallId, sqlConnection);
        }

        public static void RefreshMVBPriceInDb(int csId, SqlCrud sqlConnection)
        {
            PriceLogic.UpdateMVBPriceInDb(csId, sqlConnection);
        }

        public static void GetEditionsFromDb(SqlCrud sqlConnection)
        {
            EditionLogic.ReadAllEditionsFromDb(sqlConnection);
        }

    }
}
