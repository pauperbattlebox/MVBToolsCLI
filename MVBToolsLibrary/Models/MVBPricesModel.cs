using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models
{
    public class MVBPricesModel
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
