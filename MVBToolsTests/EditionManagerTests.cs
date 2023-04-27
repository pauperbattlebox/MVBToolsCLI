using Moq;
using MVBToolsLibrary;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;

namespace MVBToolsTests
{
    [TestClass]
    public class EditionManagerTests
    {
        [TestMethod]
        public void TestGetEditionFromApi()
        {
            int editionId = 772;

            EditionModel edition = new EditionModel()
            {
                CsId = 772,
                CsName = "Avacyn Restored",                
                MtgJsonCode = "AVR"
            };

            Mock<IEditionDbRepository<EditionModel>> mockDbRepo = new Mock<IEditionDbRepository<EditionModel>>();

            Mock<IMvbApiEditionRepository> mockApiRepo = new Mock<IMvbApiEditionRepository>();


            mockApiRepo.Setup(e => e.Get(editionId)).ReturnsAsync(edition);

            EditionManager manager = new EditionManager(mockDbRepo.Object, mockApiRepo.Object);

            var result = manager.GetEditionFromApi(editionId).Result;

            Assert.AreEqual(result, edition);
        }
    }
}
