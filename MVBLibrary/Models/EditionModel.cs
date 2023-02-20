using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class EditionModel : IModel
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cs_id")]
        public int CsId { get; set; }

        [JsonProperty(PropertyName = "cs_name")]
        public string CsName { get; set; }

        [JsonProperty(PropertyName = "mtgjson_code")]
        public string MtgJsonCode { get; set; }


    }
}
