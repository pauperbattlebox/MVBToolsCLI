using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Models
{
    public class EditionModel
    {
        [JsonPropertyName("cs_id")]
        public int CsId { get; set; }

        [JsonPropertyName("cs_name")]
        public string CsName { get; set; }

        [JsonPropertyName("mtgjson_code")]
        public string MtgJsonCode { get; set; }
    }
}
