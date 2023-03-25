using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public class MvbApiRepository : IMvbApiRepository
    {
        private readonly IHttpClientFactory _httpClient;

        MvbApiRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> Get(string url)
        {

            var uri = new Uri(url);

            var response = await _httpClient.CreateClient().GetAsync(url);
            
            var output = await response.Content.ReadAsStringAsync();

            return output;
        }
    }
}
