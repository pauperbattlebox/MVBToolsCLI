using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Models
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
    }
}
