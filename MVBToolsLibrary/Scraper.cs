using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection.Metadata;

namespace MVBToolsLibrary
{
    public class Scraper
    {
        //public Uri uri { get; set; }        

        public string GetHtml()
        {
            //HtmlWeb web = new HtmlWeb();

            //HtmlDocument document = web.Load("https://www.cardsphere.com/sets/1304");

            ////string xpath = @"/html";
            /////html/body/div[2]/div/div[2]/h3/text()
            //string xpath = @"/html/body/script[4]";

            //var scriptString = document.DocumentNode.SelectSingleNode(xpath).InnerHtml;

            //return "HI";

            var options = new ChromeOptions()
            {
                BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
            };

            options.AddArguments(new List<string>() { "headless", "disable-gpu" });

            var browser = new ChromeDriver(options);

            browser.Navigate().GoToUrl("https://www.cardsphere.com/sets/1304");

            var title = browser.FindElements(By.XPath("/html/body/div[2]/div/div[2]/h3"))[0].Text;
            //var title = browser.FindElements(By.CssSelector("body > div.layout-content > div > div:nth-child(2) > h3"))[0].GetAttribute("innerHTML").ToString();

            return title;
        }
    }
}


