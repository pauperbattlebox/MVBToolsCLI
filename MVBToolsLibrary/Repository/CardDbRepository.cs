using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsLibrary.Repository
{
    public class CardDbRepository : IObjectManagerRepository
    {
        private DbSettings _settings;
        private readonly string _connectionString;

        public CardDbRepository(IOptions<DbSettings> settings)
        {
            _settings = settings.Value;
            _connectionString = _settings.Default;
        }
        public async Task GetAll(string mtgJsonCode)
        {
            string query = @"SELECT Name, CsId, MtgJsonCode
                            FROM dbo.Card
                            WHERE MtgJsonCode = @MtgJsonCode;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<CardModel>(query, new { MtgJsonCode = mtgJsonCode });
            }
        }

        public async Task Get(int id)
        {
            string query = @"SELECT CsId, Name, MtgJsonCode
                            FROM dbo.Card
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var row = await connection.QueryFirstOrDefaultAsync<CardModel>(query, new { CsId = id });
            }
        }

        public async Task Insert(CardModel card)
        {

            string query = @"IF NOT EXISTS
                            (SELECT CsId FROM dbo.Card WHERE CsId = @CsId)
                            BEGIN
                            INSERT INTO dbo.Card (CsId, Name, MtgjsonId, ScryfallId, MtgJsonCode)
                            VALUES (@CsId, @Name, @MtgJsonId, @ScryfallId, @MtgJsonCode)
                            END;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { card.CardshereId, card.Name, card.MtgJsonId, card.ScryfallId, card.MtgJsonCode });
            }
        }

        public Task Upsert(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
