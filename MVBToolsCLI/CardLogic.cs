using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary;
using MVBToolsLibrary.Json;
using System.Text.Json;
//using Newtonsoft.Json;

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

            string response = Utilities.CallEndpoint(endpointUrl);

            JsonHandler jsonObj = Factory.CreateJsonHandler();

            EditionCardsModel output = JsonSerializer.Deserialize<EditionCardsModel>(response);

            return output;
        }                
                
    }
}
