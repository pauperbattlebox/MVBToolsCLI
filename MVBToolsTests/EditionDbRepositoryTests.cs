using Moq;
using MVBToolsCLI;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsTests
{
    [TestClass]
    public class EditionDbRepositoryTests
    {
        [TestMethod]
        public void Get_Test()
        {
            //EditionModel model = new EditionModel()
            //{
            //    CsId = 1,
            //    CsName = "Limited Edition Alpha",
            //    MtgJsonCode = "LEA"
            //};

            //Mock<IEditionDbRepository<EditionModel>> mockRepo = new Mock<IEditionDbRepository<EditionModel>>();

            //mockRepo.Setup(e => e.Get(1)).ReturnsAsync(model);

            //var editionDbRepository = new EditionDbRepository(mockRepo.Object);

            //var edition = editionDbRepository.Get(1);

            //Assert.IsNotNull(edition);
            //Assert.Equals(model, edition);
        }        
    }
}
