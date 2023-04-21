using HtmlAgilityPack;

namespace MVBToolsLibrary
{
    public class Scraper
    {
        //public Uri uri { get; set; }        

        public HtmlNodeCollection GetHtml()
        {
            var client = new HttpClient();

            var response = client.GetStringAsync("https://www.cardsphere.com").Result.ToString();

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(response);

            //string xpath = "/html/body/nav/div/div[1]/a/img";
            string xpath = "/html";

            var html = htmlDoc.DocumentNode.SelectNodes(xpath);

            return html;
        }
    }
}
