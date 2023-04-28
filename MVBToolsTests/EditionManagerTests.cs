using Moq;
using MVBToolsLibrary;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System.Diagnostics;

namespace MVBToolsTests
{
    [TestClass]
    public class EditionManagerTests
    {
        [TestMethod]
        public void TestGetEditionFromApi()
        {

            EditionModel edition = new EditionModel()
            {
                CsId = 772,
                CsName = "Avacyn Restored",                
                MtgJsonCode = "AVR"
            };

            Mock<IEditionDbRepository<EditionModel>> mockDbRepo = new Mock<IEditionDbRepository<EditionModel>>();

            Mock<IMvbApiEditionRepository> mockApiRepo = new Mock<IMvbApiEditionRepository>();


            mockApiRepo.Setup(e => e.Get(edition.CsId)).ReturnsAsync(edition);

            EditionManager manager = new EditionManager(mockDbRepo.Object, mockApiRepo.Object);

            var result = manager.GetEditionFromApi(edition.CsId).Result;


            Assert.AreEqual(result, edition);
            Assert.IsNotNull(result);
        }
    }
}
