using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionDbRepository<EditionModel>
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
            string query = @"SELECT CardsphereId, Name
                            FROM dbo.Edition
                            WHERE CardsphereId = @CardsphereId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryFirstOrDefaultAsync<EditionModel>(query, new { CardsphereId = id });
                return rows;
            }            
        }

        public async Task Insert(EditionModel edition)
        {
            string query = @"INSERT dbo.Edition (CardsphereId, CardsphereName, MtgJsonCode)
                            VALUES (@CardsphereId, @CardsphereName, @MtgJsonCode);";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { edition.CardsphereId, edition.CardsphereName, edition.MtgJsonCode });
            }
        }
    }
}
