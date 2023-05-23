using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models.ProviderModels
{
    public class MVBPricesModel
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
