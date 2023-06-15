using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Mappers
{
    public static class ToEditionModel
    {
        public static EditionModel FromMvbEditionModel(MvbEditionModel mvbEditionModel)
        {
            EditionModel editionModel = new EditionModel();

            editionModel.CardsphereId = mvbEditionModel.CardsphereId;
            editionModel.CardsphereName = mvbEditionModel.Name;
            editionModel.MtgJsonCode = mvbEditionModel.MtgJsonCode;

            return editionModel;
        }
    }
}
