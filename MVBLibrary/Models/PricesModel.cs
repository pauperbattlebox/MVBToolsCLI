using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PricesModel
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
