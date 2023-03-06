
using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class ScryfallBulkDataModel
    {

        [JsonPropertyName("download_uri")]
        public string BulkDataUrl { get; set; }
    }
}
