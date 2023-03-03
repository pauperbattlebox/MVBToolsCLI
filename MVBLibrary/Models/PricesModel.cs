using System.Text.Json.Serialization;

namespace DataAccessLibrary.Models
{
    public class PricesModel
    {
        
        public MVBPricesModel CsPrice { get; set; }

        public ScryfallPricesModel ScryfallPrice { get; set; }
    }
}
