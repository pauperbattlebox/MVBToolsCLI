using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;

namespace MVBToolsLibrary.Repository.Db
{
    public class CardDbRepository : ICardDbRepository<MVBCardModel>
    {
        private DbSettings _settings;

        public CardDbRepository(IOptions<DbSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<IEnumerable<MVBCardModel>> GetAllById(string mtgJsonCode)
        {
            var connectionString = _settings.Default;

            string query = @"SELECT Name, CsId, MtgJsonCode
                            FROM dbo.Card
                            WHERE MtgJsonCode = @MtgJsonCode;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<MVBCardModel>(query, new { MtgJsonCode = mtgJsonCode });
                return rows;
            }
        }

        public async Task<MVBCardModel> Get(int id)
        {

            string connectionString = _settings.Default;

            string query = @"SELECT CsId, Name, MtgJsonCode
                            FROM dbo.Card
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var row = await connection.QueryFirstOrDefaultAsync<MVBCardModel>(query, new { CsId = id });
                return row;
            }            
        }

        public async Task Insert(MVBCardModel card)
        {
            string connectionString = _settings.Default;

            string query = @"IF NOT EXISTS
                            (SELECT CsId FROM dbo.Card WHERE CsId = @CsId)
                            BEGIN
                            INSERT INTO dbo.Card (CsId, Name, MtgjsonId, ScryfallId, MtgJsonCode)
                            VALUES (@CsId, @Name, @MtgJsonId, @ScryfallId, @MtgJsonCode)
                            END;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { card.CsId, card.Name, card.MtgJsonId, card.ScryfallId, card.MtgJsonCode });
            }
        }
    }
}
