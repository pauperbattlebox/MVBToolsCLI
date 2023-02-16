using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLibrary.Models
{
    public class ScryfallCardModel : IModel
    {
        [JsonProperty(PropertyName = "id")]
        public string ScryfallId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "prices")]
        public Dictionary<string, string> Prices { get; set; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "set")]
        public string ScryfallCode { get; set; }

    }
}
