using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models
{
    public class EditionModel
    {
        [JsonPropertyName("cs_id")]
        public int CaprsphereId { get; set; }

        [JsonPropertyName("cs_name")]
        public string CardsphereName { get; set; }

        [JsonPropertyName("mtgjson_code")]
        public string MtgJsonCode { get; set; }
    }
}
