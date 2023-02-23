using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary.Json;
using Newtonsoft.Json;

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

            IJsonHandler jsonObj = Factory.CreateJsonHandler();

            //EditionCardsModel model = (EditionCardsModel)Factory.CreateEditionCardsModel();

            //EditionCardsModel jsonResponse = (EditionCardsModel)jsonObj.Deserialize(response, model);

            EditionCardsModel output = JsonConvert.DeserializeObject<EditionCardsModel>(response);

            return output;
        }                
                
    }
}
