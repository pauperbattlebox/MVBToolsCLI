using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository
{
    public class EditionRepository : IRepository<EditionModel, int>
    {
        SqlCrud sql = new SqlCrud("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public async Task<IEnumerable<EditionModel>> GetAll()
        {
            return await sql.GetAllEditions();
        }

        public async Task<EditionModel> Get(int id)
        {
            return await sql.GetEdition(id);
        }

        public EditionModel Insert(EditionModel entity)
        {
            return sql.CreateSet(entity);
        }
    }
}
