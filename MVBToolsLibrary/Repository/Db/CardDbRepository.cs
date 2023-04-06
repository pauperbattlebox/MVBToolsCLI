using System.Data.SqlClient;
using System.Data;
using Dapper;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Interfaces;

namespace MVBToolsLibrary.Repository.Db
{
    public class CardDbRepository : ICardDbRepository<MVBCardModel>
    {

        private readonly IDbSettings _dbSettings;        

        public CardDbRepository(IDbSettings dbSettings)
        {
            this._dbSettings = dbSettings;
        }
        public async Task<IEnumerable<MVBCardModel>> GetAllById(string mtgJsonCode)
        {
            string connectionString = _dbSettings.Default;            

            string query = @"SELECT Name, MtgJsonCode
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

            string connectionString = _dbSettings.Default;

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
            string connectionString = _dbSettings.Default;

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
