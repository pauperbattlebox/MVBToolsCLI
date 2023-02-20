using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Endpoint
{
    public class ScryfallEndpoint : Endpoint, IScryfallEndpoint
    {
        public override string _baseUrl { get; } = "https://api.scryfall.com";

        public string CardById(string id)
        {
            return $"{_baseUrl}/cards/{id}";
        }

        public string AllCards()
        {
            return $"{_baseUrl}/bulk-data/all_cards";
        }
    }
}
