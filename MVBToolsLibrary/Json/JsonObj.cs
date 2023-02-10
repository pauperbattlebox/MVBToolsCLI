using DataAccessLibrary.Models;
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
    public class JsonObj
    {
        //props
        public string Filename { get; set; }

        //constructors
        public JsonObj() { }

        public JsonObj(string filename)
        {
            Filename = filename;
        }
        

        //methods       

        public IModel Deserialize(string json)
        {
            var output = JsonConvert.DeserializeObject<IModel>(json);            
            Console.WriteLine(output);

            return output;
        }
        public JToken ReadJsonFromFile(JsonObj jsonObj)
        {
            using (StreamReader reader = File.OpenText(jsonObj.Filename))
            {
                JObject obj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));

                return (JArray)obj["data"]["cards"];
            }
        }
        public List<CardModel> MatchByName(JToken jToken, string nameToMatch)
        {
            var newCard = from card in jToken
                          where card["name"].ToString() == nameToMatch
                          select card;

            List<CardModel> output = new List<CardModel>();

            foreach (var card in newCard)
            {
                CardModel cardModel = card.ToObject<CardModel>();

                output.Add(cardModel);
            }

            return output;
        }
    }
}
