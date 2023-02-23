using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;

namespace DataAccessLibrary.Models
{
    public class ScryfallBulkDataModel : IModel
    {

        [JsonProperty(PropertyName = "download_uri")]
        public string BulkDataUrl { get; set; }
    }
}
