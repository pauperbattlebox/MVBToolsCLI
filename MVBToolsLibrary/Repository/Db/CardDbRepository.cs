using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;

namespace MVBToolsLibrary.Repository.Db
{
    public class CardDbRepository : ICardDbRepository<CardModel>
    {
        private DbSettings _settings;
        private readonly string _connectionString;

        public CardDbRepository(IOptions<DbSettings> settings)
        {
            _settings = settings.Value;
            _connectionString = _settings.Default;
        }
        public async Task<IEnumerable<CardModel>> GetAllById(string mtgJsonCode)
        {
            string query = @"SELECT Name, CardsphereId, MtgJsonCode
                            FROM dbo.Card
                            WHERE MtgJsonCode = @MtgJsonCode;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<CardModel>(query, new { MtgJsonCode = mtgJsonCode });
                return rows;
            }
        }

        public async Task<CardModel> Get(int id)
        {
            string query = @"SELECT CardsphereId, Name, MtgJsonCode
                            FROM dbo.Card
                            WHERE CardsphereId = @CardsphereId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var row = await connection.QueryFirstOrDefaultAsync<CardModel>(query, new { CardsphereId = id });
                return row;
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
                connection.Execute(query, new { card.CardsphereId, card.Name, card.MtgJsonId, card.ScryfallId, card.MtgJsonCode });
            }
        }
    }
}
