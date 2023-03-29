using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository.Api
{
    public class MvbApiCardRepository : IMvbApiCardRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiCardRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<MVBCardModel> GetCard(string url)
        {
            var uri = new Uri(url);

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MVBCardModel>(text);

            return output;
        }

        
    }
}
