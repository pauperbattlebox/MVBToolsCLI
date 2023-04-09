using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models
{
    public class ScryfallCardModel
    {
        [JsonPropertyName("id")]
        public string ScryfallId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("prices")]
        public ScryfallPricesModel Prices { get; set; }

        [JsonPropertyName("set")]
        public string ScryfallCode { get; set; }

    }
}
