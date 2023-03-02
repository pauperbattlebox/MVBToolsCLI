using DataAccessLibrary.Models;
using DataAccessLibrary;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class Card
    {
        public static void AddCardToDb(SqlCrud sql, MVBCardModel cardModel)
        {
            sql.AddCardByEdition(cardModel);
        }

        public static EditionCardsModel GetCardsFromMVBAPI(int editionId)
        {
            string endpointUrl = Edition.GetMVBEditionEndpoint(editionId);

            string response = HttpClientFactory.CallEndpoint(endpointUrl);

            EditionCardsModel output = JsonSerializer.Deserialize<EditionCardsModel>(response);

            return output;
        }

        public static void AddFilteredCardsToDb(SqlCrud sqlConnection, EditionCardsModel model)
        {
            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                AddCardToDb(sqlConnection, card);
            }
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
