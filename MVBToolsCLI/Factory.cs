using DataAccessLibrary.Models;
using DataAccessLibrary.Models.Interfaces;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Endpoint.Interfaces;
using MVBToolsLibrary.Json;

namespace MVBToolsCLI
{
    public static class Factory
    {
        public static Commands CreateNewCommands()
        {
            return new Commands(CreateJsonHandler());
        }
        public static IMvbEndpoint CreateMvbEndpoint()
        {
            return new MvbEndpoint();
        }
        public static IScryfallEndpoint CreateScryfallEndpoint()
        {
            return new ScryfallEndpoint();
        }        
        public static JsonHandler CreateJsonHandler()
        {
            return new JsonHandler();
        }

    }
}
