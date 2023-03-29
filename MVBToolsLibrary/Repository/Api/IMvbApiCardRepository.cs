using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiCardRepository
    {
        Task<MVBCardModel> GetCard(string url);
        
    }
}
