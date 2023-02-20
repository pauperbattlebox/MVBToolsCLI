using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class MVBPricesModel : IModel
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cs_id")]
        public int CsId { get; set; }

        [JsonProperty(PropertyName = "prices")]
        //public decimal CsPrice { get; set; }
        public Dictionary<string, string> Prices { get; set; } = new Dictionary<string, string>();

        public float ScryfallPrice { get; set; }
        public float CardKingdomPrice { get; set; }        
    }
}
