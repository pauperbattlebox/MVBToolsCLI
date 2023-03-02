using DataAccessLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVBToolsLibrary.Json
{
    public class JsonHandler : IJsonHandler
    {
        //props
        public string Filename { get; set; }

        //constructors
        public JsonHandler() { }

        public JsonHandler(string filename)
        {
            Filename = filename;
        }

        //methods       
                
        public JToken ReadMtgJsonFile(JsonHandler jsonObj)
        {
            using (StreamReader reader = File.OpenText(jsonObj.Filename))
            {
                JObject obj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                return (JArray)obj["data"]["cards"];
            }
        }
        public List<MVBCardModel> MatchByName(JToken jToken, string nameToMatch)
        {
            var newCard = from card in jToken
                          where card["name"].ToString() == nameToMatch
                          select card;

            List<MVBCardModel> output = new List<MVBCardModel>();

            foreach (var card in newCard)
            {
                MVBCardModel cardModel = card.ToObject<MVBCardModel>();

                output.Add(cardModel);
            }

            return output;
        }
    }
}
