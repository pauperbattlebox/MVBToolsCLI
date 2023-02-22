namespace DataAccessLibrary.Models.Interfaces
{
    public interface IEditionCardsModel
    {
        List<MVBCardModel> Cards { get; set; }
        EditionModel Edition { get; set; }
    }
}