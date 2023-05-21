using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionDbRepository<EditionModel>
    {        
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EditionDbRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }        
                
        public async Task<IEnumerable<EditionModel>> GetAll()
        {
            //string connectionString = _configuration.GetConnectionString("Default");

            string query = @"SELECT * FROM dbo.Edition;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<EditionModel>(query, new { });
                return rows;
            }
        }

        public async Task<EditionModel> Get(int id)
        {
            //string connectionString= _configuration.GetConnectionString("Default");

            string query = @"SELECT CsId, Name
                            FROM dbo.Edition
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryFirstOrDefaultAsync<EditionModel>(query, new { CsId = id });
                return rows;
            }            
        }

        public async Task Insert(EditionModel edition)
        {

            //string connectionString = _configuration.GetConnectionString("Default");

            string query = @"INSERT dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CsId, @CsName, @MtgJsonCode);";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { edition.CsId, edition.CsName, edition.MtgJsonCode });
            }
        }
    }
}
