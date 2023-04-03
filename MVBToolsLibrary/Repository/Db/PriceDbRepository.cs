using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Interfaces;

namespace MVBToolsLibrary.Repository.Db
{
    public class PriceDbRepository : IPriceDbRepository
    {
        private readonly IDbSettings _dbSettings;

        public PriceDbRepository(IDbSettings dbSettings)
        {
            this._dbSettings = dbSettings;
        }
        public async Task UpdateCardsphere(int id, decimal price)
        {
            var connectionString = _dbSettings.Default;

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
                connection.Execute(query, new { CsPrice = price, CsId = id });
            }            
        }

        public async Task UpdateScryfall(string scryfallId, int csId, decimal price)
        {
            var connectionString = _dbSettings.Default;

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
            var connectionString = _dbSettings.Default;

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
