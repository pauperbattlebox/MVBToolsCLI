using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class PriceLogic
    {
        public static void AddPriceToDb(SqlCrud sql, MVBPricesModel pricesModel)
        {
            sql.CreatePrice(pricesModel);
        }

        public static decimal GetScryfallPriceFromAPI(string scryfallId)
        {
            ScryfallEndpoint scryfallEndpoint = new ScryfallEndpoint();

            string endpointUrl = scryfallEndpoint.CardById(scryfallId);

            string response = Utils.CallEndpoint(endpointUrl);

            JsonHandler jsonObj = new JsonHandler();

            ScryfallCardModel model = new ScryfallCardModel();

            ScryfallCardModel jsonResponse = (ScryfallCardModel)jsonObj.Deserialize(response, model);

            return decimal.Parse(jsonResponse.Prices["usd"]);
        }

        public static void UpdateScryfallPriceInDb(SqlCrud sql, string scryfallId)
        {
            decimal price = GetScryfallPriceFromAPI(scryfallId);

            sql.UpdateScryfallPrice(scryfallId, price);

        }

    }
}
