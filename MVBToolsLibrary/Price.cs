using DataAccessLibrary.Models;
using DataAccessLibrary;
using MVBToolsLibrary.Endpoint.Interfaces;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class Price
    {
        public static decimal GetScryfallPriceFromAPI(string scryfallId)
        {
            ScryfallEndpoint scryfallEndpoint = new ScryfallEndpoint();

            string endpointUrl = scryfallEndpoint.CardById(scryfallId);

            string response = Utilities.CallEndpoint(endpointUrl);            

            ScryfallCardModel output = JsonSerializer.Deserialize<ScryfallCardModel>(response);
            
            return Decimal.Parse(output.Prices.Price);
        }
        public static decimal GetMVBPriceFromAPI(int csId)
        {
            MvbEndpoint mvbEndpoint = new MvbEndpoint();

            string endpointUrl = mvbEndpoint.CardById(csId);

            string response = Utilities.CallEndpoint(endpointUrl);            

            MVBCardModel output = JsonSerializer.Deserialize<MVBCardModel>(response);
                        
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
