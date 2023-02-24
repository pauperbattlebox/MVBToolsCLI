using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using MVBToolsLibrary.Endpoint;
using Newtonsoft.Json;

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

            ScryfallBulkDataModel url = JsonConvert.DeserializeObject<ScryfallBulkDataModel>(response);

            string output = url.BulkDataUrl;

            return output;
        }

    }
}
