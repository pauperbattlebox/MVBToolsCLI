using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V110.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Scrapers
{
    public class CardsphereCardPage
    {
        public string BaseUrl { get; private set; } = "https://www.cardsphere.com/sets";
        public string Id { get; set; }
        private readonly ChromeDriverSetup ChromeDriverSetup = new ChromeDriverSetup();
        private ChromeDriver Driver;

        public CardsphereCardPage(string id)
        {            
            Driver = ChromeDriverSetup.BuildChromeDriver();
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
    }
}
