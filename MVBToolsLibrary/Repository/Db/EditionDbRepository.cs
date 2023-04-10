using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionDbRepository<EditionModel>
    {   

        private readonly IDbSettings _dbSettings;
        private readonly IConfiguration _config;

        public EditionDbRepository(IDbSettings dbSettings, IConfiguration config)
        {            
            this._dbSettings = dbSettings;
            _config = config;
        }
                
        public async Task<IEnumerable<EditionModel>> GetAll()
        {
            //string cs = _config.GetConnectionString();
            string connectionString = _dbSettings.Default;

            string query = @"SELECT * FROM dbo.Edition;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<EditionModel>(query, new { });
                return rows;
            }
        }

        public async Task<EditionModel> Get(int id)
        {

            var connectionString = _dbSettings.Default;

            string query = @"SELECT CsId, Name
                            FROM dbo.Edition
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryFirstOrDefaultAsync<EditionModel>(query, new { CsId = id });
                return rows;
            }            
        }

        public async Task Insert(EditionModel edition)
        {

            string connectionString = _dbSettings.Default;

            string query = @"INSERT dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CsId, @CsName, @MtgJsonCode);";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { edition.CsId, edition.CsName, edition.MtgJsonCode });
            }
        }
    }
}
