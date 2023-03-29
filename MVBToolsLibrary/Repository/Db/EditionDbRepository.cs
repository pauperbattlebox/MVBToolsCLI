using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionDbRepository<EditionModel>
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

        public async Task<EditionModel> Insert(EditionModel entity)
        {
            return await sql.CreateSet(entity);
        }
    }
}
