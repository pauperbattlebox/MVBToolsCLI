using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;

namespace MVBToolsLibrary.Repository
{
    public class EditionDbRepository : IObjectManagerRepository
    {
        private DbSettings _settings;
        private readonly string _connectionString;

        public EditionDbRepository(IOptions<DbSettings> settings)
        {
            _settings = settings.Value;
            _connectionString = _settings.Default;
        }

        public async Task GetAll()
        {
            string query = @"SELECT * FROM dbo.Edition;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<EditionModel>(query, new { });                
            }
        }

        public async Task Get(int id)
        {
            string query = @"SELECT CsId, Name
                            FROM dbo.Edition
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryFirstOrDefaultAsync<EditionModel>(query, new { CsId = id });                
            }
        }

        public async Task Insert(EditionModel edition)
        {
            string query = @"INSERT dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CsId, @CsName, @MtgJsonCode);";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { edition.CaprsphereId, edition.CardsphereName, edition.MtgJsonCode });
            }
        }

        public async Task Upsert(EditionModel edition)
        {
            string query = @"IF NOT EXISTS
                            (SELECT CsId FROM dbo.Edition WHERE CsId = @CaprsphereId)
                            BEGIN
                            INSERT INTO dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CaprsphereId, @CardsphereName, @MtgJsonCode)
                            END
                            ELSE
                            BEGIN
                            UPDATE dbo.Edition SET MtgJsonCode = @MtgJsonCode
                            WHERE CsId = @CaprsphereId
                            END;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { edition.CaprsphereId, edition.CardsphereName, edition.MtgJsonCode });            
            }
        }

        public async Task Delete(int id)
        {
            string query = @"DELETE FROM dbo.Edition
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { CsId = id });
            }
        }
    }
}
