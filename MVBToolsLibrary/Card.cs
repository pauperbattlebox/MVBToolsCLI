using DataAccessLibrary.Models;
using DataAccessLibrary;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class Card
    {
        public static void AddCardToDb(SqlCrud sql, MVBCardModel cardModel)
        {
            sql.AddCardByEdition(cardModel);
        }

        public static EditionCardsModel GetCardsFromMVBAPI(int editionId)
        {
            string endpointUrl = Edition.GetMVBEditionEndpoint(editionId);

            string response = Utilities.CallEndpoint(endpointUrl);

            //JsonHandler jsonObj = Factory.CreateJsonHandler();

            EditionCardsModel output = JsonSerializer.Deserialize<EditionCardsModel>(response);

            return output;
        }
    }
}
