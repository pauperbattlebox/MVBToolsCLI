using Moq;
using MVBToolsLibrary;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary.Scrapers;
using System.Diagnostics;
using Bogus;
using OpenQA.Selenium.Chrome;

namespace MVBToolsTests
{
    [TestClass]
    public class EditionManagerUnitTests
    {
        [TestMethod]
        public void TestGetEditionFromApi()
        {
            var faker = new Faker<EditionModel>()
                .RuleFor(x => x.CsId, f => f.Random.Int(1, 1000))
                .RuleFor(x => x.CsName, f => f.Random.Words())
                .RuleFor(x => x.MtgJsonCode, f => f.Lorem.Word());

            var edition = faker.Generate();

            Mock<IEditionDbRepository<EditionModel>> mockDbRepo = new Mock<IEditionDbRepository<EditionModel>>();
            Mock<IMvbApiEditionRepository> mockApiRepo = new Mock<IMvbApiEditionRepository>();
            Mock<IChromeDriverSetup> mockDriverSetup = new Mock<IChromeDriverSetup>();

            mockApiRepo.Setup(e => e.Get(edition.CsId)).ReturnsAsync(edition);

            EditionManager manager = new EditionManager(mockDbRepo.Object, mockApiRepo.Object, mockDriverSetup.Object);


            var result = manager.GetEditionFromApi(edition.CsId).Result;
            Debug.WriteLine($"{result.CsId}, {result.CsName}, {result.MtgJsonCode}");
            Debug.WriteLine($"{edition.CsId}, {edition.CsName}, {result.MtgJsonCode}");


            Assert.AreEqual(result, edition);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestScrapeEditionFromWebpage()
        {
            ChromeDriverSetup driverSetup = new ChromeDriverSetup();
            EditionModel editionModel = new EditionModel()
            {
                CsId = 1304,
                CsName = "Kaldheim"
            };
            CardsphereCardPage cardsphereCardPage = new CardsphereCardPage(editionModel.CsId, driverSetup);
            ChromeDriver driver = cardsphereCardPage.ScrapePage();
            cardsphereCardPage.Driver = driver;

            var result = cardsphereCardPage.GetEditionTitle();

            Debug.WriteLine($"{editionModel.CsName} - {editionModel.CsId}");
            Debug.WriteLine($"{result}");

            Assert.AreEqual(result, editionModel.CsName);
            Assert.IsNotNull(result);
        }
    }
}
