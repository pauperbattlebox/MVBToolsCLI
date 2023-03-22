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
        
        private readonly IConsoleWriter _consoleWriter;
        private readonly IRepository<EditionModel, int> _editionRepository;


        public Edition(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }        

        public Edition(IRepository<EditionModel, int> repository)
        {
            _editionRepository = repository;
        }
        
        

        public async Task<IEnumerable<EditionModel>> ReadAllEditionsFromDb()
        {
            //IRepository<EditionModel, int> repository = new EditionRepository();

            //return await repository.GetAll();           

            return await _editionRepository.GetAll();
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

            string response = HttpClientFactory.CallEndpoint(endpoint);

            EditionModel output = JsonSerializer.Deserialize<EditionModel>(response);

            return output;
        }
    }
}
