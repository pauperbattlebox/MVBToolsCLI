using Dapper;
using DataAccessLibrary.Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public class SqlDataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public List<DbCardModel> LoadCardPriceData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<DbCardModel> rows = connection.Query<DbCardModel, DbPriceModel, DbCardModel>(sqlStatement, (card, price) => { card.CsPrice = price.CsPrice; card.ScryfallPrice = price.ScryfallPrice; return card; }, parameters, splitOn: "splitter").ToList();

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
