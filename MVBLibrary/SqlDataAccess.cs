using Dapper;
using DataAccessLibrary.Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public class SqlDataAccess
    {
        public async Task<IEnumerable<T>> LoadAsyncData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sqlStatement, parameters);
                return rows;
            }
        }
        public async Task<T> LoadAsyncSingleData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QuerySingleAsync<T>(sqlStatement, parameters);

                return rows;
            }
        }

        public IEnumerable<DbCardModel> LoadCardPriceData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<DbCardModel> rows = connection.Query<DbCardModel, DbPriceModel, DbCardModel>(sqlStatement, (card, price) => { card.CsPrice = price.CsPrice; card.ScryfallPrice = price.ScryfallPrice; return card; }, parameters, splitOn: "splitter").ToList();

                return rows;
            }
        }
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
