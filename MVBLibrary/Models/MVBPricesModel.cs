using DataAccessLibrary.Models.Interfaces;
using Newtonsoft.Json;

namespace DataAccessLibrary.Models
{
    public class MVBPricesModel : IModel
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cs_id")]
        public int CsId { get; set; }

        [JsonProperty(PropertyName = "prices")]        
        public Dictionary<string, string> Prices { get; set; } = new Dictionary<string, string>();
    }
}
