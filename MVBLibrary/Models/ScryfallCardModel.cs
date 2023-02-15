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
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "lang")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public Dictionary<string, string> Prices { get; set; } = new Dictionary<string, string>();

        //public ScryfallPricesModel USD { get; set; }

        //public ScryfallPricesModel Prices { get; set; }

    }
}
