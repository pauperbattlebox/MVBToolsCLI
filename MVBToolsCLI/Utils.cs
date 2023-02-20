using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class Utils
    {
        public static string GetConnectionString(string connectionStringName = "Default")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string output = config.GetConnectionString(connectionStringName);

            return output;
        }

        public static string CallEndpoint(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
        }

        public string GetBulkDataURLFromScryfall()
        {
            ScryfallEndpoint endpoint = (ScryfallEndpoint)Factory.CreateScryfallEndpoint();

            string response = Utils.CallEndpoint(endpoint.AllCards());

            var jsonHandler = Factory.CreateJsonHandler();

            ScryfallBulkDataModel model = (ScryfallBulkDataModel)Factory.CreateScryfallBulkDataModel();

            ScryfallBulkDataModel url = (ScryfallBulkDataModel)jsonHandler.Deserialize(response, model);

            string output = url.BulkDataUrl;

            return output;
        }

    }
}
