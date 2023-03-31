using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
