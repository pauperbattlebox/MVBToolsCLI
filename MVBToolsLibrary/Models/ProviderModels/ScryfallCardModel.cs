using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models.ProviderModels
{
    public class ScryfallCardModel
    {
        [JsonPropertyName("id")]
        public string ScryfallId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("prices")]
        public ScryfallPriceModel Prices { get; set; }

        [JsonPropertyName("set")]
        public string ScryfallCode { get; set; }

    }
}
