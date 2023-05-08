using Bogus;
using Moq;
using MVBToolsLibrary;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System.Diagnostics;

namespace MVBToolsTests
{
    [TestClass]
    public class PriceManagerUnitTests
    {
        [TestMethod]
        public void TestGetPriceFromMvbApi()
        {
            var fakerCard = new Faker<MVBCardModel>()
                .RuleFor(x => x.CsId, f => f.Random.Int(5));

            var fakerPrice = new Faker<MVBPricesModel>()
                .RuleFor(x => x.Price, f => f.Random.Decimal());

            var card = fakerCard.Generate();
            var price = fakerPrice.Generate().Price;

            Mock<IPriceDbRepository> mockDbRepo = new Mock<IPriceDbRepository>();
            Mock<IMvbApiPriceRepository> mockMvbApi = new Mock<IMvbApiPriceRepository>();
            Mock<IScryfallApiPriceRepository> mockScryfallApi = new Mock<IScryfallApiPriceRepository>();

            mockMvbApi.Setup(p => p.Get(card.CsId)).ReturnsAsync(price);

            PriceManager manager = new PriceManager(mockDbRepo.Object, mockMvbApi.Object, mockScryfallApi.Object);

            var result = manager.GetPriceFromMvbApi(card.CsId).Result;
            Debug.WriteLine($"{result}, {price}");

            Assert.AreEqual(result, price);
        }
    }
}
