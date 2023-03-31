using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Models
{
    public class ScryfallPricesModel
    {
        [JsonPropertyName("usd")]
        public string Price { get; set; }

    }
}
