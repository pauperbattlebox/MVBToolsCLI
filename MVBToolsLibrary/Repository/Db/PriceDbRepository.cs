using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Repository.Db
{
    public class PriceDbRepository : IPriceDbRepository
    {
        SqlCrud sql = new SqlCrud("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                
        public async Task Update(int id, decimal price)
        {
            await sql.UpdateMvbPrice(id, price);
        }

        public async Task<IEnumerable<DbCardModel>> Get(int id)
        {
            return await sql.GetCardPrice(id);
        }

    }
}
