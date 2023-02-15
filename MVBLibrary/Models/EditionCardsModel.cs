using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class EditionCardsModel : IModel
    {        
        public EditionModel Edition { get; set; }
        public List<MVBCardModel> Cards { get; set; } = new List<MVBCardModel>();
    }
}
