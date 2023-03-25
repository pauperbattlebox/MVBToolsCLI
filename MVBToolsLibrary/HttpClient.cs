

namespace MVBToolsLibrary
{
    public class HttpClient
    {
        public static string CallEndpoint(string url)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                return json;
            }
        }
    }
}
