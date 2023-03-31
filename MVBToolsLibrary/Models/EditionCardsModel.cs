using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Models
{
    public class EditionCardsModel
    {
        public EditionModel Edition { get; set; } = new EditionModel();

        [JsonPropertyName("cards")]
        public List<MVBCardModel> Cards { get; set; } = new List<MVBCardModel>();
    }
}
