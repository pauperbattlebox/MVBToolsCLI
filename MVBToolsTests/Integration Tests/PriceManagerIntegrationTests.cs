using Bogus;
using MVBToolsLibrary.Models;
using MVBToolsTests.Integration_Tests.Mock_Api_Servers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsTests.Integration_Tests
{
    [TestClass]
    public class PriceManagerIntegrationTests
    {
        private readonly ScryfallApiMockServer _scryfallApiMockServer = new();

        [TestMethod]
        public async Task GetPriceFromApi()
        {
            var client = new RestClient("http://localhost:1234");
            var cardModel = GenerateScryfallCardModel();
            cardModel.Prices = GenerateScryfallPricesModel();
            var price = Decimal.Parse(cardModel.Prices.Price);

            _scryfallApiMockServer.Start();
            _scryfallApiMockServer.ReturnPrice(cardModel);
            RestRequest request = new RestRequest($"/{cardModel.ScryfallId}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            Debug.WriteLine(response.Content);
            Debug.WriteLine(price.ToString());

            Assert.IsTrue(response.Content.Contains(price.ToString()));
            Assert.IsTrue(price.GetType() ==  typeof(decimal));

            _scryfallApiMockServer.Dispose();

        }

        private ScryfallCardModel GenerateScryfallCardModel()
        {
            var fakeCardModel = new Faker<ScryfallCardModel>()
                .RuleFor(x => x.ScryfallId, f => Guid.NewGuid().ToString())
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.ScryfallCode, f => f.Lorem.Word());

            var cardModel = fakeCardModel.Generate();
            return cardModel;
        }

        private ScryfallPricesModel GenerateScryfallPricesModel()
        {
            var fakePriceModel = new Faker<ScryfallPricesModel>()
                .RuleFor(x => x.Price, f => f.Random.Decimal().ToString());

            var priceModel = fakePriceModel.Generate();
            return priceModel;
        }
    }
}
