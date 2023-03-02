using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public class HttpClientFactory
    {
        public static string CallEndpoint(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
        }
    }
}
