using DataAccessLibrary.Models;
using DataAccessLibrary;
using MVBToolsLibrary.Endpoint;
using System.Text.Json;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Repository.Db;

namespace MVBToolsLibrary
{
    public class Edition
    {
        
        private readonly IConsoleWriter _consoleWriter;
        private readonly IEditionDbRepository<EditionModel> _editionDbRepository;

        public Edition(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        } 
        public string GetMVBEditionEndpoint(int editionId)
        {
            var endpoint = new MvbEndpoint();

            return endpoint.EditionById(editionId);
        }

        public void AddEditionToDb(SqlCrud sql, EditionModel editionModel)
        {
            sql.CreateSet(editionModel);

            _consoleWriter.WriteLineToConsole($"{editionModel.CsName} added to ye olde database");
        }

        public EditionModel GetEditionFromMvb(int editionId)
        {
            string endpoint = GetMVBEditionEndpoint(editionId);

            string response = HttpClient.CallEndpoint(endpoint);            

            EditionModel output = JsonSerializer.Deserialize<EditionModel>(response);

            return output;
        }
    }
}
