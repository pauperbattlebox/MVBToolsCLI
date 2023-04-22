using OpenQA.Selenium.Chrome;

namespace MVBToolsLibrary.Scrapers
{
    public interface IChromeDriverSetup
    {
        ChromeDriver BuildChromeDriver();
    }
}