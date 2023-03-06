using DataAccessLibrary.Models;
using DataAccessLibrary;
using MVBToolsLibrary.Endpoint;
using System.Text.Json;
using MVBToolsLibrary.Interfaces;

namespace MVBToolsLibrary
{
    public class Price
    {

        IConsoleWriter _consoleWriter;

        public Price(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }
        public static decimal GetScryfallPriceFromAPI(string scryfallId)
        {
            ScryfallEndpoint scryfallEndpoint = new ScryfallEndpoint();

            string endpointUrl = scryfallEndpoint.CardById(scryfallId);

            string response = HttpClientFactory.CallEndpoint(endpointUrl);            

            ScryfallCardModel output = JsonSerializer.Deserialize<ScryfallCardModel>(response);
            
            return Decimal.Parse(output.Prices.Price);
        }
        public static decimal GetMVBPriceFromAPI(int csId)
        {
            MvbEndpoint mvbEndpoint = new MvbEndpoint();

            string endpointUrl = mvbEndpoint.CardById(csId);

            string response = HttpClientFactory.CallEndpoint(endpointUrl);

            MVBCardModel output = JsonSerializer.Deserialize<MVBCardModel>(response);
                        
            return output.Prices.Price;
        }

        public void UpdateScryfallPriceInDb(string scryfallId, SqlCrud sql)
        {
            decimal price = GetScryfallPriceFromAPI(scryfallId);

            sql.UpdateScryfallPrice(scryfallId, price);

            _consoleWriter.WriteLineToConsole($"Price for card ID {scryfallId} updated.");

        }
        public void UpdateMVBPriceInDb(int csId, SqlCrud sql)
        {
            decimal price = GetMVBPriceFromAPI(csId);

            sql.UpdateMvbPrice(csId, price);

            _consoleWriter.WriteLineToConsole($"Price for card ID {csId} updated.");

        }
    }
}
