using MVBToolsLibrary.Models;

namespace MVBToolsLibrary.Interfaces
{
    public interface IEditionManager
    {
        Task<IEnumerable<EditionModel>> GetAllEditionsFromDb();
    }
}