using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVBToolsLibrary.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IdentifierModel Identifiers { get; set; }
        public PriceModel Prices { get; set; }
    }
}
