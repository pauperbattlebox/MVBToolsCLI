using System.Text.Json.Serialization;

namespace MVBToolsLibrary.Models
{
    public class EditionCardsModel
    {
        public EditionModel Edition { get; set; } = new EditionModel();

        [JsonPropertyName("cards")]
        public List<MVBCardModel> Cards { get; set; } = new List<MVBCardModel>();
    }
}
