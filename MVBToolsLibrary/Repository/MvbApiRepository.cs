using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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


        public async Task<MVBCardModel> GetCard(string url)
        {
            var uri = new Uri(url);

            var response = await _httpClient.CreateClient().GetAsync(uri);
            
            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MVBCardModel>(text);

            return output;
        }

        public async Task<EditionModel> GetEdition(int id)
        {
            var uri = new Uri($"https://www.multiversebridge.com/api/v1/sets/cs/{id}");

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<EditionModel>(text);

            return output;
        }

        public async Task<EditionCardsModel> GetCardsByEdition(int id)
        {
            var uri = new Uri($"https://www.multiversebridge.com/api/v1/sets/cs/{id}");

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<EditionCardsModel>(text);

            return output;
        }
    }
}
