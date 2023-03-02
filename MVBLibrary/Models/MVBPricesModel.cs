using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class MVBPricesModel
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        public decimal CsPrice { get; set; }

        public decimal ScryfallPrice { get; set; }
    }
}
