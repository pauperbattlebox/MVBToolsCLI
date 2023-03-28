using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public interface IMvbApiRepository
    {
        Task<MVBCardModel> GetCard(string url);
        Task<EditionModel> GetEdition(int id);
        Task<EditionCardsModel> GetCardsByEdition(int id);
    }
}
