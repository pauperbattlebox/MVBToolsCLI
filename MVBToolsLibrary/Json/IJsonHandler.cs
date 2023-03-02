using DataAccessLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVBToolsLibrary.Json
{
    public interface IJsonHandler
    {
        string Filename { get; set; }
                
        List<MVBCardModel> MatchByName(JToken jToken, string nameToMatch);
        JToken ReadMtgJsonFile(JsonHandler jsonObj);
    }
}