using DataAccessLibrary.Models;
using DataAccessLibrary.Models.Interfaces;
using MVBToolsCLI.Interfaces;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Endpoint.Interfaces;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public static class Factory
    {
        public static Commands CreateNewCommands()
        {
            return new Commands(CreateJsonHandler(), CreateEditionModel());
        }
        public static IMvbEndpoint CreateMvbEndpoint()
        {
            return new MvbEndpoint();
        }
        public static IScryfallEndpoint CreateScryfallEndpoint()
        {
            return new ScryfallEndpoint();
        }
        public static IEditionModel CreateEditionModel()
        {
            return new EditionModel();
        }
        public static IModel CreateScryfallCardModel()
        {
            return new ScryfallCardModel();
        }
        public static IEditionCardsModel CreateEditionCardsModel()
        {
            return new EditionCardsModel();
        }
        public static IModel CreateMVBPricesModel()
        {
            return new MVBPricesModel();
        }
        public static IModel CreateScryfallBulkDataModel()
        {
            return new ScryfallBulkDataModel();
        }

        public static IJsonHandler CreateJsonHandler()
        {
            return new JsonHandler();
        }

    }
}
