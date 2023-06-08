using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models.ProviderModels
{
    public class MvbPriceModel
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
