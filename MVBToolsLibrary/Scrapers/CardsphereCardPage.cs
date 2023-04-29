using MVBToolsLibrary.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V110.Page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Scrapers
{
    public class CardsphereCardPage
    {
        public string BaseUrl { get; private set; } = "https://www.cardsphere.com/sets";
        public string Id { get; set; }
        private readonly IChromeDriverSetup _driverSetup;
        private ChromeDriver Driver;

        public CardsphereCardPage(string id, IChromeDriverSetup driverSetup)
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
            //var title = browser.FindElements(By.CssSelector("body > div.layout-content > div > div:nth-child(2) > h3"))[0].GetAttribute("innerHTML").ToString();

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

                MVBPricesModel pricesModel= new MVBPricesModel();

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