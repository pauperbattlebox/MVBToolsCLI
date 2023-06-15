using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary.Mappers
{
    public static class ToCardModel
    {
        public static CardModel FromMvbCardModel(MvbCardModel mvbCardModel)
        {
            CardModel cardModel = new CardModel();

            cardModel.CardshereId = mvbCardModel.CsId;
            cardModel.Name = mvbCardModel.Name;
            cardModel.MtgJsonId = mvbCardModel.MtgJsonId;
            cardModel.ScryfallId = mvbCardModel.ScryfallId;
            cardModel.MtgJsonCode = mvbCardModel.MtgJsonCode;
            cardModel.CardspherePrice = mvbCardModel.Prices.Price;

            return cardModel;
        }

        public static CardModel FromScryfallCardModel(ScryfallCardModel scryfallModel)
        {
            CardModel cardModel = new CardModel();
            
            cardModel.Name = scryfallModel.Name;
            cardModel.ScryfallId = scryfallModel.ScryfallId;
            cardModel.MtgJsonCode = scryfallModel.ScryfallCode;
            cardModel.CardspherePrice = decimal.Parse(scryfallModel.Prices.Price);

            return cardModel;
        }
    }
}
