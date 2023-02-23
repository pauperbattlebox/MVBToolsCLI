using DataAccessLibrary.Models.Interfaces;

namespace DataAccessLibrary.Models
{
    public class EditionCardsModel : IEditionCardsModel, IModel
    {
        public EditionModel Edition { get; set; }
        public List<MVBCardModel> Cards { get; set; } = new List<MVBCardModel>();
    }
}
