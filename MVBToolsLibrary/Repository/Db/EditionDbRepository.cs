using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionRepository<EditionModel>
    {        
        private DbSettings _settings;
        private readonly string _connectionString;

        public EditionDbRepository(IOptions<DbSettings> settings)
        {
            _settings = settings.Value;
            _connectionString = _settings.Default;
        }        
                
        public async Task<IEnumerable<EditionModel>> GetAll()
        {
            string query = @"SELECT * FROM dbo.Edition;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<EditionModel>(query, new { });
                return rows;
            }
        }

        public async Task<EditionModel> Get(int id)
        {
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
            string query = @"INSERT dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CsId, @CsName, @MtgJsonCode);";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { edition.CsId, edition.CsName, edition.MtgJsonCode });
            }
        }
    }
}
