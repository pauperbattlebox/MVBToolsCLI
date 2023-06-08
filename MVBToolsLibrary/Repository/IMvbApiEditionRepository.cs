using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public interface IMvbApiEditionRepository
    {
        Task<Stream> Get(int id);
        Task<Stream> GetCardsByEdition(int editionId);
    }
}
