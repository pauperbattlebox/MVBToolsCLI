using DataAccessLibrary.Models;
using DataAccessLibrary;
using MVBToolsLibrary.Endpoint;
using System.Text.Json;
using MVBToolsLibrary.Interfaces;

namespace MVBToolsLibrary
{
    public class Edition
    {
        IConsoleWriter _consoleWriter;

        public Edition(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }
        public void ReadAllEditionsFromDb(SqlCrud sql)
        {
            var rows = sql.GetAllEditions();

            foreach (var row in rows)
            {
                _consoleWriter.WriteLineToConsole($"{row.CsName} - {row.MtgJsonCode}");
            }
        }        
        public string GetMVBEditionEndpoint(int editionId)
        {
            var endpoint = new MvbEndpoint();

            return endpoint.EditionById(editionId);
        }

        public void AddEditionToDb(SqlCrud sql, EditionModel editionModel, IConsoleWriter consoleWriter)
        {
            sql.CreateSet(editionModel);

            _consoleWriter.WriteLineToConsole($"{editionModel.CsName} added to ye olde database");

        }

        public EditionModel GetEditionFromMvb(int editionId)
        {
            string endpoint = GetMVBEditionEndpoint(editionId);

            string response = HttpClientFactory.CallEndpoint(endpoint);

            EditionModel output = JsonSerializer.Deserialize<EditionModel>(response);

            return output;
        }
    }
}
