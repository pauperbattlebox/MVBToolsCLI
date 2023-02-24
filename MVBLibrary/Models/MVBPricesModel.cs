using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class MVBPricesModel
    {
        //[JsonPropertyName("price")]
        //public Dictionary<string, decimal> Price { get; set; } = new Dictionary<string, decimal>();

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }
}
