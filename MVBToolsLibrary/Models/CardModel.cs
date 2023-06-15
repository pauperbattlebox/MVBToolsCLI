using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Models
{
    public class CardModel
    {        
        public string Name { get; set; }
        public int CardsphereId { get; set; }
        public string MtgJsonId { get; set; }
        public string ScryfallId { get; set; }
        public string MtgJsonCode { get; set; }
        public decimal CardspherePrice { get; set; }
        public decimal ScryfallPrice { get; set; }
    }
}
