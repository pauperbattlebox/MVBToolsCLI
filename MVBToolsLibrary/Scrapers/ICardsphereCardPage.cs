using MVBToolsLibrary.Models;
using OpenQA.Selenium.Chrome;

namespace MVBToolsLibrary.Scrapers
{
    public interface ICardsphereCardPage
    {
        string BaseUrl { get; }
        int Id { get; set; }

        List<MVBCardModel> GetCardsAndPrices();
        string GetEditionTitle();
        ChromeDriver ScrapePage();
    }
}