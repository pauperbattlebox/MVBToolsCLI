using MVBToolsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiEditionRepository
    {
        Task<EditionModel> Get(int id);
        Task<EditionCardsModel> GetCardsByEdition(int id);
    }
}
