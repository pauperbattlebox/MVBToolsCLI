using DataAccessLibrary;
using DataAccessLibrary.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionDbRepository<EditionModel>
    {        
        SqlCrud sql = new SqlCrud("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public async Task<IEnumerable<EditionModel>> GetAll()
        {
            string query = @"SELECT * FROM dbo.Edition;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<EditionModel>(query, new { });
                return rows;
            }
        }

        public async Task<IEnumerable<EditionModel>> Get(int id)
        {
            string query = @"SELECT CsId, Name
                            FROM dbo.Edition
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<EditionModel>(query, new { CsId = id });
                return rows;
            }            
        }

        public async Task<EditionModel> Insert(EditionModel entity)
        {
            return await sql.CreateSet(entity);
        }
    }
}
