using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsCLI.Interfaces;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVBToolsCLI
{
    public class CardLogic
    {
        
        private static void AddCardToDb(SqlCrud sql, MVBCardModel cardModel)
        {
            sql.AddCardByEdition(cardModel);
        }

        public static EditionCardsModel GetCardsFromMVBAPI(int editionId)
        {
            string endpointUrl = EditionLogic.GetMVBEditionEndpoint(editionId);

            string response = Utils.CallEndpoint(endpointUrl);

            JsonHandler jsonObj = new JsonHandler();

            EditionCardsModel model = new EditionCardsModel();
                
            EditionCardsModel jsonResponse = (EditionCardsModel)jsonObj.Deserialize(response, model);

            return jsonResponse;
        }
                
        public static void AddMultipleCardsToDb(int editionId, SqlCrud sqlConnection)
        {
            EditionCardsModel model = GetCardsFromMVBAPI(editionId);

            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                Console.WriteLine($"{card.Name} added to db!");
                AddCardToDb(sqlConnection, card);
            }
        }
        public string GetBulkDataURLFromScryfall()
        {
            ScryfallEndpoint endpoint = new ScryfallEndpoint();

            string response = Utils.CallEndpoint(endpoint.AllCards());

            JsonHandler jsonHandler = new JsonHandler();

            ScryfallBulkDataModel model = new ScryfallBulkDataModel();

            ScryfallBulkDataModel url = (ScryfallBulkDataModel)jsonHandler.Deserialize(response, model);

            string output = url.BulkDataUrl;
            
            return output;
        }        
    }
}
