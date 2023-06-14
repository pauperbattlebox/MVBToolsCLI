
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiCardRepository
    {
        Task<MvbCardModel> GetCard(int id, Func<int, string, string> buildUrl);
        
    }
}
