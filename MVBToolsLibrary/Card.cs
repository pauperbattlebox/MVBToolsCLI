using DataAccessLibrary.Models;
using DataAccessLibrary;
using System.Text.Json;
using MVBToolsLibrary.Endpoint;

namespace MVBToolsLibrary
{
    public class Card
    {
        public static void AddCardToDb(SqlCrud sql, MVBCardModel cardModel, IConsoleWriter consoleWriter)
        {
            sql.AddCardByEdition(cardModel);

            consoleWriter.WriteLineToConsole($"{cardModel.Name} was added to the db!");
        }

        public static EditionCardsModel GetCardsFromMVBAPI(int editionId)
        {
            string endpointUrl = Edition.GetMVBEditionEndpoint(editionId);

            string response = HttpClientFactory.CallEndpoint(endpointUrl);

            EditionCardsModel output = JsonSerializer.Deserialize<EditionCardsModel>(response);

            return output;
        }

        public static void GetAllCardsFromMVBAPI()
        {
            var endpoint = new MvbEndpoint();

            string endpointUrl = endpoint.AllCards();
                        
            string response = HttpClientFactory.CallEndpoint(endpointUrl);

            var json = JsonSerializer.Deserialize<List<MVBCardModel>>(response).Where(id => id.CsId != null);

            for (int i = 0; i > 5; i++)
            {
                Console.WriteLine(json);
            }

            //return json;            
        }

        public static void AddFilteredCardsToDb(SqlCrud sqlConnection, EditionCardsModel model, IConsoleWriter consoleWriter)
        {
            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                AddCardToDb(sqlConnection, card, consoleWriter);
            }
        }

        public static void ReadCardFromDb(SqlCrud sqlConnection, int csId, IConsoleWriter consoleWriter)
        {
            var card = sqlConnection.GetCard(csId);

            consoleWriter.WriteLineToConsole(card.Name);

        }

        public static void ReadCardPriceFromDb(SqlCrud sqlConnection, int csId, IConsoleWriter consoleWriter)
        {
            var card = sqlConnection.GetCardPrice(csId);

            consoleWriter.WriteLineToConsole($"{card.Name}: Cardsphere Price - {card.CsPrice}, Scryfall Price - {card.ScryfallPrice}");
        }

        //public string GetBulkDataURLFromScryfall()
        //{
        //    ScryfallEndpoint endpoint = (ScryfallEndpoint)Factory.CreateScryfallEndpoint();

        //    string response = Utilities.CallEndpoint(endpoint.AllCards());

        //    var jsonHandler = Factory.CreateJsonHandler();

        //    ScryfallBulkDataModel url = JsonConvert.DeserializeObject<ScryfallBulkDataModel>(response);

        //    string output = url.BulkDataUrl;

        //    return output;
        //}
    }
}
