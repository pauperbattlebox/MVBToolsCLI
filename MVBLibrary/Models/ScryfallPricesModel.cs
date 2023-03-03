using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class ScryfallPricesModel
    {
        [JsonPropertyName("usd")]
        public string Price { get; set; }
                
    }
}
