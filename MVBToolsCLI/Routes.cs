﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class Routes
    {
        private static readonly Dictionary<string, string> routes = new Dictionary<string, string>()
        {
            { "cardsById", "cards/cs" }
        };
        
        public static string BuildUrl(int id, string endpoint)
        {
            var param = routes[endpoint];

            var output = $"https://www.multiversebridge.com/api/v1/{param}/{id}";

            return output;
        }
    }
}
