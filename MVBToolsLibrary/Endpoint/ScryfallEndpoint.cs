using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Endpoint
{
    public class ScryfallEndpoint : Endpoint
    {
        public override string _baseUrl { get; } = "https://api.scryfall.com";

        public string CardById(int id)
        {
            return $"{_baseUrl}/cards/{id}";
        }
    }
}
