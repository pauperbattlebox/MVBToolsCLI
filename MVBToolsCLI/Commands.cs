using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class Commands
    {
        public static void AddNewEditionToDb(int editionId, SqlCrud sqlConnection)
        {
            string endpoint = EditionLogic.GetMVBEditionEndpoint(editionId);

            string response = Utils.CallEndpoint(endpoint);

            IJsonHandler jsonObj = Factory.CreateJsonHandler();

            EditionModel model = (EditionModel)Factory.CreateEditionModel();

            EditionModel jsonResponse = (EditionModel)jsonObj.Deserialize(response, model);

            EditionLogic.AddEditionToDb(sqlConnection, jsonResponse);
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
