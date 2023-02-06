using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class SetCardsModel
    {
        public SetsModel Sets { get; set; }
        public List<CardsModel> Cards { get; set; } = new List<CardsModel>();
    }
}
