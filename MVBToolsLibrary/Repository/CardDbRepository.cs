using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository
{
    public class CardDbRepository : ICardDbRepository<MVBCardModel>
    {
        SqlCrud sql = new SqlCrud("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public async Task<IEnumerable<MVBCardModel>> GetAllById(int editionId)
        {
            return await sql.GetAllCardsByEditionId(editionId);
        }

        public async Task<MVBCardModel> Get(int id)
        {
            return await sql.GetCard(id);
        }

        public async Task<MVBCardModel> Insert(MVBCardModel entity)
        {
            return await sql.CreateCard(entity);
        }

        public async Task<MVBCardModel> Update(MVBCardModel entity)
        {
            return await sql.UpdateCard(entity);
        }

    }
}
