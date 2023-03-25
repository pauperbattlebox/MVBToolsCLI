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

        IConsoleWriter _consoleWriter;
        IFileReader _filereader;

        public Card(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public Card(IConsoleWriter consoleWriter, IFileReader filereader)
        {
            _consoleWriter = consoleWriter;
            _filereader = filereader;
        }
        public void AddCardToDb(SqlCrud sql, MVBCardModel cardModel)
        {
            sql.CreateCard(cardModel);

            _consoleWriter.WriteLineToConsole($"{cardModel.Name} was added to the db!");
        }

        public EditionCardsModel GetCardsFromMVBAPI(int editionId, IConsoleWriter consoleWriter)
        {
            Edition edition = new Edition(consoleWriter);
            
            string endpointUrl = edition.GetMVBEditionEndpoint(editionId);

            string response = HttpClientFactory.CallEndpoint(endpointUrl);

            EditionCardsModel output = JsonSerializer.Deserialize<EditionCardsModel>(response);

            return output;
        }

        public IEnumerable<MVBCardModel> ReadCardsFromMvbJsonFile(string fileName)
        {
            var json = _filereader.ReadFile(fileName);

            var output = JsonSerializer.Deserialize<IEnumerable<MVBCardModel>>(json);

            return output;
        }

        public void AddFilteredCardsToDb(SqlCrud sqlConnection, EditionCardsModel model)
        {
            var filteredCards = from card in model.Cards
                                where card.IsFoil == false && card.MtgJsonId != null
                                select card;

            foreach (var card in filteredCards)
            {
                AddCardToDb(sqlConnection, card);
            }
        }

        public async Task ReadCardFromDb(SqlCrud sqlConnection, int csId)
        {
            var card = sqlConnection.GetCard(csId).Result;

            _consoleWriter.WriteLineToConsole(card.Name);

        }

        public void ReadCardPriceFromDb(SqlCrud sqlConnection, int csId)
        {
            var card = sqlConnection.GetCardPrice(csId);

            _consoleWriter.WriteLineToConsole($"{card.Name}: Cardsphere Price - {card.CsPrice}, Scryfall Price - {card.ScryfallPrice}");
        }        
    }
}
