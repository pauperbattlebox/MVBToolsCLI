using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Endpoint
{
    public class MtgJsonEndpoint : Endpoint
    {
        public override string _baseUrl { get; } = "https://mtgjson.com/api/v5/";
    }
}
