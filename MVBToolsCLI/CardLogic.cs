using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class CardLogic
    {
        private static void AddNewCardToDb(SqlCrud sql, CardModel cardModel)
        {
            sql.AddCardByEdition(cardModel);
        }

        public static EditionCardsModel GetCardsFromJson(int editionId)
        {
            string endpointUrl = EditionLogic.GetEditionFromAPI(editionId);

            string response = Utils.CallEndpoint(endpointUrl);

            EditionCardsJson jsonObj = new EditionCardsJson();            

            EditionCardsModel model = jsonObj.Deserialize(response);

            return model;          
        }

        public static void AddMultipleCardsToDb(int editionId, SqlCrud sqlConnection)
        {
            EditionCardsModel model = GetCardsFromJson(editionId);

            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                Console.WriteLine(card.Name);
                AddNewCardToDb(sqlConnection, card);
            }
        }


    }
}
