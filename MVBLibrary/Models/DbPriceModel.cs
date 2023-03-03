using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class DbPriceModel
    {
        public int CsId { get; set; }

        public decimal CsPrice {  get; set; }

        public decimal ScryfallPrice { get; set; }

        public decimal CardKingdomPrice { get; set; }
    }
}
