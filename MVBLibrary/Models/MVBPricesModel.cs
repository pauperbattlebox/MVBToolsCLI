using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class MVBPricesModel
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
