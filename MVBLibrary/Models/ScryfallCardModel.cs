using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;

namespace DataAccessLibrary.Models
{
    public class ScryfallCardModel : IModel
    {
        [JsonProperty(PropertyName = "id")]
        public string ScryfallId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "prices")]
        public Dictionary<string, string> Prices { get; set; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "set")]
        public string ScryfallCode { get; set; }

    }
}
