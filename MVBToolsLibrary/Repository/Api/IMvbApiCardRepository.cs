
using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiCardRepository
    {
        Task<MVBCardModel> GetCard(int id, Func<int, string, string> buildUrl);
        
    }
}
