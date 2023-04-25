using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Interfaces
{
    public interface ICardManager
    {
        Task<MVBCardModel> GetCardFromDb(int id);
    }
}