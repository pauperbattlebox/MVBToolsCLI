using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository
{
    public class MvbApiRepository : IMvbApiRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<MVBCardModel> Get(string url)
        {
            var uri = new Uri(url);

            var response = await _httpClient.CreateClient().GetAsync(uri);
            
            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MVBCardModel>(text);

            return output;
        }
    }
}
