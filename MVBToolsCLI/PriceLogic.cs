using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class PriceLogic
    {
        public static void AddPriceToDb(SqlCrud sql, PricesModel pricesModel)
        {
            sql.CreatePrice(pricesModel);
        }
    }
}
