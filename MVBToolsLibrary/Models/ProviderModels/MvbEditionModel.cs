using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Models.ProviderModels
{
    public class MvbEditionModel
    {
        [JsonPropertyName("cs_id")]
        public int CardsphereId { get; set; }

        [JsonPropertyName("cs_name")]
        public string Name { get; set; }

        [JsonPropertyName("mtgjson_code")]
        public string MtgJsonCode { get; set; }
    }
}
