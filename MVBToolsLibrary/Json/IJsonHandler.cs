using DataAccessLibrary.Models;
using Newtonsoft.Json.Linq;

namespace MVBToolsLibrary.Json
{
    public interface IJsonHandler
    {
        string Filename { get; set; }

        IModel Deserialize<T>(string json, T model);
        List<MVBCardModel> MatchByName(JToken jToken, string nameToMatch);
        JToken ReadMtgJsonFile(JsonHandler jsonObj);
    }
}