using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class MVBAllCardsModel
    {
        [JsonPropertyName("cards")]
        public List<MVBCardModel> Cards { get; set; } = new List<MVBCardModel>();
    }
}
