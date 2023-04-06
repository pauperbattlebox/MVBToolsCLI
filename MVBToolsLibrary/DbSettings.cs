using MVBToolsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public class DbSettings : IDbSettings
    {
        public string Default { get; set; }
    }
}
