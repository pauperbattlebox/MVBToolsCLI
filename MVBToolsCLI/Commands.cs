using DataAccessLibrary;
using DataAccessLibrary.Models;
using DataAccessLibrary.Models.Interfaces;
using MVBToolsLibrary.Json;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace MVBToolsCLI
{
    public class Commands
    {
        IJsonHandler _jsonHandler;
        IEditionModel _editionModel;
        public Commands(IJsonHandler jsonHandler, IEditionModel editionModel)
        {
            _jsonHandler = jsonHandler;
            _editionModel = editionModel;
        }        

        public void AddNewEditionToDb(int editionId, SqlCrud sqlConnection)
        {
            string endpoint = EditionLogic.GetMVBEditionEndpoint(editionId);

            string response = Utils.CallEndpoint(endpoint);

            EditionModel jsonResponse = (EditionModel)_jsonHandler.Deserialize(response, _editionModel);

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
