using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;
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
        //public Dictionary<string, dynamic> Prices { get; set; } = new Dictionary<string, dynamic>();
        public MVBPricesModel Prices { get; set; }

    }
}
