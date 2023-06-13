using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using Microsoft.Extensions.Options;
using static Dapper.SqlMapper;

namespace MVBToolsLibrary.Repository.Db
{
    public class PriceDbRepository : IPriceDbRepository
    {
        private DbSettings _settings;
        private readonly string _connectionString;

        public PriceDbRepository(IOptions<DbSettings> settings)
        {
            _settings = settings.Value;
            _connectionString = _settings.Default;
        }
        public async Task UpdateCardsphere(int id, decimal price)
        {
            string query = @"IF NOT EXISTS
                            (SELECT CsId FROM dbo.Prices WHERE CsId = @CsId)
                            BEGIN
                            INSERT INTO dbo.Prices (CsId, CsPrice)
                            VALUES (@CsId, @CsPrice)
                            END
                            ELSE
                            BEGIN
                            UPDATE dbo.Prices SET CsPrice = @CsPrice
                            WHERE CsId = @CsId
                            END;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { CsId = id, CsPrice = price });
            }            
        }

        public async Task UpdateScryfall(string scryfallId, int csId, decimal price)
        {
            string query = @"IF NOT EXISTS
                            (SELECT CsId FROM dbo.Prices WHERE CsId = @CsId)
                            BEGIN
                            INSERT INTO dbo.Prices (CsId, ScryfallPrice)
                            VALUES (@CsId, @ScryfallPrice)
                            END
                            ELSE
                            BEGIN
                            UPDATE dbo.Prices
                            SET ScryfallPrice = @ScryfallPrice
                            WHERE CsId = @CsId
                            END;";


            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { ScryfallPrice = price, CsId = csId, ScryfallId = scryfallId });
            }
        }

        public async Task<CardModel> Get(int id)
        {
            string query = @"SELECT c.CsId, c.Name, 0 as splitter, p.CsPrice, p.ScryfallPrice
                            FROM dbo.Card as c
                            LEFT JOIN dbo.Prices AS p ON c.CsId = p.CsId
                            WHERE c.CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {                
                 var rows = await connection.QueryFirstOrDefaultAsync<CardModel>(query, new { CsId = id });
                 return rows;
            }
        }
    }
}
