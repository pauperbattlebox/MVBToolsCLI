using MVBToolsLibrary.Models;
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

        public List<MVBCardModel> GetCardsAndPrices()
        {
            var priceList = Driver.FindElements(By.CssSelector(".cards .cs-row"));

            List<MVBCardModel> cardsList = new List<MVBCardModel>();

            foreach (var item in priceList)
            {
                MVBCardModel cardModel = new MVBCardModel();

                cardModel.Name = item.FindElement(By.ClassName("cardpeek")).Text;

                cardModel.CsId = Int32.Parse(item.FindElement(By.ClassName("cardpeek")).GetAttribute("href").Split("/cards/")[1]);

                MVBPricesModel pricesModel = new MVBPricesModel();

                var price = item.FindElement(By.CssSelector(".card-price")).Text.Replace("$", "");

                decimal output;

                if (Decimal.TryParse(price, out output))
                {
                    pricesModel.Price = output;
                }
                else
                {
                    pricesModel.Price = output;
                };

                cardModel.Prices = pricesModel;

                cardsList.Add(cardModel);
            }

            return cardsList;
        }
    }
}