using DataAccessLibrary.Models;
using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public dynamic Deserialize<T>(string json, T model)
        {
            dynamic output = JsonConvert.DeserializeObject<T>(json);

            return output;
        }        
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
