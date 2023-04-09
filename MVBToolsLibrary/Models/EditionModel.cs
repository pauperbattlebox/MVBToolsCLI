using System.Text.Json.Serialization;

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
