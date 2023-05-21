using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MVBToolsLibrary.Repository.Db
{
    public class PriceDbRepository : IPriceDbRepository
    {
        private readonly string _connectionString;

        public PriceDbRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task UpdateCardsphere(int id, decimal price)
        {
            var connectionString = _connectionString;

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

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { CsId = id, CsPrice = price });
            }            
        }

        public async Task UpdateScryfall(string scryfallId, int csId, decimal price)
        {
            var connectionString = _connectionString;

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


            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { ScryfallPrice = price, CsId = csId, ScryfallId = scryfallId });
            }
        }

        public async Task<DbCardModel> Get(int id)
        {
            var connectionString = _connectionString;

            string query = @"SELECT c.CsId, c.Name, 0 as splitter, p.CsPrice, p.ScryfallPrice
                            FROM dbo.Card as c
                            LEFT JOIN dbo.Prices AS p ON c.CsId = p.CsId
                            WHERE c.CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {                
                 var rows = await connection.QueryFirstOrDefaultAsync<DbCardModel>(query, new { CsId = id });
                 return rows;
            }
        }
    }
}
