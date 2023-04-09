using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models
{
    public class ScryfallPricesModel
    {
        [JsonPropertyName("usd")]
        public string Price { get; set; }

    }
}
