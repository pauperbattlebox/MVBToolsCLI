using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class MVBCardModel
    {
        
        [JsonPropertyName("cs_id")]
        public int CsId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mtgjson_id")]        
        public string MtgJsonId { get; set; }

        [JsonPropertyName("scryfall_id")]
        public string ScryfallId { get; set; }

        [JsonPropertyName("mtgjson_code")]
        public string MtgJsonCode { get; set; }

        [JsonPropertyName("is_foil")]
        public bool IsFoil { get; set; }

        [JsonPropertyName("prices")]
        public MVBPricesModel Prices { get; set; }

        public PricesModel AllPrices { get; set; }
    }
}
