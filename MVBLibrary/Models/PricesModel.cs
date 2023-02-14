using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PricesModel
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cs_id")]
        public int CsId { get; set; }

        [JsonProperty(PropertyName = "price")]
        public float CsPrice { get; set; }

        public float ScryfallPrice { get; set; }
        public float CardKingdomPrice { get; set; }        
    }
}
