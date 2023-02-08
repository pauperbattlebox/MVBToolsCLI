using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public int CsId { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }        
        public string MtgjsonId { get; set; }
        public string ScryfallId { get; set; }
        public string MtgJsonCode { get; set; }

    }
}
