using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models.ProviderModels
{
    public class ScryfallPriceModel
    {
        [JsonPropertyName("usd")]
        public string Price { get; set; }

    }
}
