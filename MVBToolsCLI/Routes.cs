using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class Routes
    {
        public static string BuildUrl(int id)
        {
            return $"https://www.multiversebridge.com/api/v1/cards/cs/{id}";
        }
    }
}
