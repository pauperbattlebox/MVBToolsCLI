using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsCLI.Interfaces;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MVBToolsCLI
{
    public class CardLogic
    {
        
        public static void AddCardToDb(SqlCrud sql, MVBCardModel cardModel)
        {
            sql.AddCardByEdition(cardModel);
        }

        public static EditionCardsModel GetCardsFromMVBAPI(int editionId)
        {
            string endpointUrl = EditionLogic.GetMVBEditionEndpoint(editionId);

            string response = Utils.CallEndpoint(endpointUrl);

            var jsonObj = Factory.CreateJsonHandler();

            EditionCardsModel model = (EditionCardsModel)Factory.CreateEditionCardsModel();

            EditionCardsModel jsonResponse = (EditionCardsModel)jsonObj.Deserialize(response, model);

            return jsonResponse;
        }                
                
    }
}
