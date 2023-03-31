using DataAccessLibrary;
using DataAccessLibrary.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace MVBToolsLibrary.Repository.Db
{
    public class CardDbRepository : ICardDbRepository<MVBCardModel>
    {
        
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MVB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<IEnumerable<MVBCardModel>> GetAllById(string mtgJsonCode)
        {
            string query = @"SELECT c.Name, c.MtgJsonCode, e.csName
                            FROM dbo.Card as c
                            LEFT JOIN dbo.Edition as e
                            ON c.MtgJsonCode = e.MtgJsonCode
                            WHERE e.MtgJsonCode = @MtgJsonCode;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<MVBCardModel>(query, new { MtgJsonCode = mtgJsonCode });
                return rows;
            }
        }

        public async Task<IEnumerable<MVBCardModel>> Get(int id)
        {
            string query = @"SELECT CsId, Name, MtgJsonCode
                            FROM dbo.Card
                            WHERE CsId = @CsId;";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<MVBCardModel>(query, new { CsId = id });
                return rows;
            }            
        }

        public async Task Insert(MVBCardModel card)
        {
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
