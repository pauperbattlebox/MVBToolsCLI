using MVBToolsLibrary.Models;

namespace MVBToolsLibrary
{
    public interface IEditionManager
    {
        Task<IEnumerable<EditionModel>> GetAllEditionsFromDb();
    }
}