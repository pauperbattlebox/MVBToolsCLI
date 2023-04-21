using Moq;
using MVBToolsCLI;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsTests
{
    [TestClass]
    public class MvbApiEditionRespositoryTests
    {
        [TestMethod]
        public void Get_Edition_Test()
        {
            EditionModel model = new EditionModel
            {
                CsId = 1,
                CsName = "Limited Edition Alpha",
                MtgJsonCode = "LEA"
            };
        }
    }
}
