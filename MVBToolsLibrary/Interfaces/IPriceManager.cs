using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Interfaces
{
    public interface IPriceManager
    {
        Task<DbCardModel> GetPriceFromDb(int id);
    }
}