using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cs_id")]
        public int CsId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mtgjson_id")]        
        public string MtgJsonId { get; set; }

        [JsonProperty(PropertyName = "scryfall_id")]
        public string ScryfallId { get; set; }

        [JsonProperty(PropertyName = "mtgjson_code")]
        public string MtgJsonCode { get; set; }

        [JsonProperty(PropertyName = "is_foil")]
        public bool IsFoil { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public PricesModel Prices { get; set; }

    }
}
