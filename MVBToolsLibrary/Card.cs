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

        public IEnumerable<MVBCardModel> ReadCardsFromMvbJsonFile(string fileName)
        {
            var json = _filereader.ReadFile(fileName);

            var output = JsonSerializer.Deserialize<IEnumerable<MVBCardModel>>(json);

            return output;
        }    
    }
}
