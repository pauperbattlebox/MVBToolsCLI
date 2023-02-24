using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Endpoint.Interfaces;
using MVBToolsLibrary.Json;
using Newtonsoft.Json;

namespace MVBToolsCLI
{
    public class PriceLogic
    {
        
        public static decimal GetScryfallPriceFromAPI(string scryfallId)
        {
            ScryfallEndpoint scryfallEndpoint = (ScryfallEndpoint)Factory.CreateScryfallEndpoint();

            string endpointUrl = scryfallEndpoint.CardById(scryfallId);

            string response = Utils.CallEndpoint(endpointUrl);

            IJsonHandler jsonObj = Factory.CreateJsonHandler();

            ScryfallCardModel output = JsonConvert.DeserializeObject<ScryfallCardModel>(response);

            return decimal.Parse(output.Prices["usd"]);
        }
        public static decimal GetMVBPriceFromAPI(int csId)
        {
            IMvbEndpoint mvbEndpoint = Factory.CreateMvbEndpoint();

            string endpointUrl = mvbEndpoint.CardById(csId);

            string response = Utils.CallEndpoint(endpointUrl);

            IJsonHandler jsonObj = Factory.CreateJsonHandler();

            MVBPricesModel output = JsonConvert.DeserializeObject<MVBPricesModel>(response);

            return output.Price;
        }

        public static void UpdateScryfallPriceInDb(string scryfallId, SqlCrud sql)
        {
            decimal price = GetScryfallPriceFromAPI(scryfallId);

            sql.UpdateScryfallPrice(scryfallId, price);

        }
        public static void UpdateMVBPriceInDb(int csId, SqlCrud sql)
        {
            decimal price = GetMVBPriceFromAPI(csId);

            sql.UpdateMvbPrice(csId, price);

        }
    }
}
