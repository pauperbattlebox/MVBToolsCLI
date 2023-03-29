﻿using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository.Api
{
    public class ScryfallApiPriceRepository : IScryfallApiPriceRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public ScryfallApiPriceRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> Get(string id)
        {

            var uri = new Uri($"https://api.scryfall.com/cards/{id}");

            var response = await _httpClient.CreateClient().GetAsync(uri);

            var text = await response.Content.ReadAsStringAsync();

            var output = JsonSerializer.Deserialize<ScryfallCardModel>(text);

            var price = Decimal.Parse(output.Prices.Price);

            return price;
        }
    }
}