using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class SetsModel
    {
        public int Id { get; set; }
        public int CsId { get; set; }
        public string CsName { get; set; }
        public string MtgjsonCode { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
