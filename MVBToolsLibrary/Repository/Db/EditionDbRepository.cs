using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MVBToolsLibrary.Interfaces;

namespace MVBToolsLibrary.Repository.Db
{
    public class EditionDbRepository : IEditionDbRepository<EditionModel>
    {        
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly IConfiguration _configuration;        
        private readonly IDbSettings _dbSettings;
        //private readonly IConfiguration _config;

        public EditionDbRepository(IConfiguration configuration, IDbSettings dbSettings)
        {
            this._configuration = configuration;
            this._dbSettings = dbSettings;
            //this._dbSettings = dbSettings.Value;
        }
        
        public async Task<IEnumerable<EditionModel>> GetAll()
        {

            //Console.WriteLine(_configuration["ConnectionStrings:Default"]);
            //Console.WriteLine( _configuration.GetConnectionString("Default"));

            var cs = _dbSettings.Default;

            //Console.WriteLine($"CS: {cs}");

            string query = @"SELECT * FROM dbo.Edition;";

            using (IDbConnection connection = new SqlConnection(cs))
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

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryFirstOrDefaultAsync<EditionModel>(query, new { CsId = id });
                return rows;
            }            
        }

        public async Task Insert(EditionModel edition)
        {
            string query = @"INSERT dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CsId, @CsName, @MtgJsonCode);";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { edition.CsId, edition.CsName, edition.MtgJsonCode });
            }
        }
    }
}
