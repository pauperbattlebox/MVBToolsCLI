using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository
{
    public class CardDbRepository : ICardDbRepository<MVBCardModel>
    {
        SqlCrud sql = new SqlCrud("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public async Task<IEnumerable<MVBCardModel>> GetAllById(string mtgJsonCode)
        {
            return await sql.GetAllCardsByEditionId(mtgJsonCode);
        }

        public async Task<MVBCardModel> Get(int id)
        {
            return await sql.GetCard(id);
        }

        public async Task Insert(MVBCardModel entity)
        {
            sql.CreateCard(entity);
        }
    }
}
