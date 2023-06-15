using MVBToolsLibrary.Mappers;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Models.ProviderModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MVBToolsLibrary.Scrapers
{
    public class CardsphereCardPage : ICardsphereCardPage
    {
        public string BaseUrl { get; private set; } = "https://www.cardsphere.com/sets";
        public int Id { get; set; }
        private readonly IChromeDriverSetup _driverSetup;
        public ChromeDriver Driver { get; set; }

        public CardsphereCardPage(int id, IChromeDriverSetup driverSetup)
        {
            _driverSetup = driverSetup;
            Driver = _driverSetup.BuildChromeDriver();
            Id = id;
        }

        public ChromeDriver ScrapePage()
        {
            var url = $"{BaseUrl}/{Id}";

            Driver.Navigate().GoToUrl(url);

            return Driver;
        }

        public string GetEditionTitle()
        {
            var title = Driver.FindElements(By.XPath("/html/body/div[2]/div/div[2]/h3"))[0].Text;

            return title;
        }

        public List<CardModel> GetCardsAndPrices()
        {
            var priceList = Driver.FindElements(By.CssSelector(".cards .cs-row"));

            List<CardModel> cardsList = new List<CardModel>();

            foreach (var item in priceList)
            {
                CardModel cardModel = new CardModel();

                cardModel.Name = item.FindElement(By.ClassName("cardpeek")).Text;

                cardModel.CardshereId = Int32.Parse(item.FindElement(By.ClassName("cardpeek")).GetAttribute("href").Split("/cards/")[1]);

                MvbPriceModel priceModel = new MvbPriceModel();

                var price = item.FindElement(By.CssSelector(".card-price")).Text.Replace("$", "");

                decimal output;

                if (Decimal.TryParse(price, out output))
                {
                    priceModel.Price = output;
                }
                else
                {
                    priceModel.Price = output;
                };

                cardModel.CardspherePrice = priceModel.Price;

                cardsList.Add(cardModel);
            }

            return cardsList;
        }
    }
}