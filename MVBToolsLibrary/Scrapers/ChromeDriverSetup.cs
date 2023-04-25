using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection.Metadata;

namespace MVBToolsLibrary.Scrapers
{
    public class ChromeDriverSetup : IChromeDriverSetup
    {
        public ChromeDriver BuildChromeDriver()
        {
            var options = new ChromeOptions()
            {
                BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
            };

            options.AddArguments(new List<string>() { "headless", "disable-gpu" });

            var browser = new ChromeDriver(options);

            return browser;
        }
    }
}