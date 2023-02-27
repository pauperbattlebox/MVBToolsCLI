using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Endpoint.Interfaces;
using MVBToolsLibrary.Json;
using System.Text.Json;

namespace MVBToolsCLI
{
    public class PriceLogic
    {
        
        public static decimal GetScryfallPriceFromAPI(string scryfallId)
        {
            ScryfallEndpoint scryfallEndpoint = (ScryfallEndpoint)Factory.CreateScryfallEndpoint();

            string endpointUrl = scryfallEndpoint.CardById(scryfallId);

            string response = Utilities.CallEndpoint(endpointUrl);

            IJsonHandler jsonObj = Factory.CreateJsonHandler();

            ScryfallCardModel output = JsonSerializer.Deserialize<ScryfallCardModel>(response);

            //return decimal.Parse(output.Prices["usd"]);

            return Decimal.Parse(output.Prices.Price);
        }
        public static decimal GetMVBPriceFromAPI(int csId)
        {
            IMvbEndpoint mvbEndpoint = Factory.CreateMvbEndpoint();

            string endpointUrl = mvbEndpoint.CardById(csId);

            string response = Utilities.CallEndpoint(endpointUrl);

            IJsonHandler jsonObj = Factory.CreateJsonHandler();

            MVBCardModel output = JsonSerializer.Deserialize<MVBCardModel>(response);

            //return Decimal.Parse(output.Price["price"]);
            return output.Prices.Price;
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
