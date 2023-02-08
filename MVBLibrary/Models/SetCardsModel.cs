using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class SetCardsModel
    {
        public SetModel Sets { get; set; }
        public List<CardModel> Cards { get; set; } = new List<CardModel>();
    }
}
