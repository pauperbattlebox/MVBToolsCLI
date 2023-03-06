using DataAccessLibrary.Models;
using DataAccessLibrary;
using System.Text.Json;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using MVBToolsLibrary.Interfaces;

namespace MVBToolsLibrary
{
    public class Card
    {
        public static void AddCardToDb(SqlCrud sql, MVBCardModel cardModel, IConsoleWriter consoleWriter)
        {
            sql.AddCard(cardModel);

            consoleWriter.WriteLineToConsole($"{cardModel.Name} was added to the db!");
        }

        public static EditionCardsModel GetCardsFromMVBAPI(int editionId)
        {
            string endpointUrl = Edition.GetMVBEditionEndpoint(editionId);

            string response = HttpClientFactory.CallEndpoint(endpointUrl);

            EditionCardsModel output = JsonSerializer.Deserialize<EditionCardsModel>(response);

            return output;
        }

        public static IEnumerable<MVBCardModel> ReadCardsFromMvbJsonFile(string fileName, IJsonHandler jsonHandler)
        {
            var json = jsonHandler.ReadFileFromJson(fileName);

            var output = JsonSerializer.Deserialize<IEnumerable<MVBCardModel>>(json);

            return output;
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
    }
}
