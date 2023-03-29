﻿using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository.Api
{
    public class MvbApiPriceRepository : IMvbApiPriceRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public MvbApiPriceRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> Get(int id)
        {
            var uri = new Uri($"https://www.multiversebridge.com/api/v1/cards/cs/{id}");

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<MVBCardModel>(text);

            var price = output.Prices.Price;

            return price;
        }

    }
}