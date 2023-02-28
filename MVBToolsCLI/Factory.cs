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
            return new Commands();
        }                
        public static JsonHandler CreateJsonHandler()
        {
            return new JsonHandler();
        }

    }
}
