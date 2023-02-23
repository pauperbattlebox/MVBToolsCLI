using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;

namespace DataAccessLibrary.Models
{
    public class EditionModel : IEditionModel, IModel
    {
        public EditionModel()
        {

        }

        public int Id { get; set; }

        [JsonProperty(PropertyName = "cs_id")]
        public int CsId { get; set; }

        [JsonProperty(PropertyName = "cs_name")]
        public string CsName { get; set; }

        [JsonProperty(PropertyName = "mtgjson_code")]
        public string MtgJsonCode { get; set; }
    }
}
