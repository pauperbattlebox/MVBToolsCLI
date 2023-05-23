using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models.ProviderModels
{
    public class ScryfallPricesModel
    {
        [JsonPropertyName("usd")]
        public string Price { get; set; }

    }
}
