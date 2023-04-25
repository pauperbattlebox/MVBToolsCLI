using MVBToolsLibrary.Models;

namespace MVBToolsLibrary
{
    public interface ICardManager
    {
        Task<MVBCardModel> GetCardFromDb(int id);
    }
}