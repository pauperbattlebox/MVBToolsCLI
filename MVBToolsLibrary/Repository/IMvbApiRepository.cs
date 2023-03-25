using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public interface IMvbApiRepository
    {
        Task<string> Get(string url);
    }
}
