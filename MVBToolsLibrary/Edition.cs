using DataAccessLibrary.Models;
using DataAccessLibrary;
using MVBToolsLibrary.Endpoint;
using System.Text.Json;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Repository;

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
            EditionRepository editionRepository = new EditionRepository();

            var editions = editionRepository.GetAll();

            foreach(var edition in editions)
            {
                Console.WriteLine($"{edition.CsName}");
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
