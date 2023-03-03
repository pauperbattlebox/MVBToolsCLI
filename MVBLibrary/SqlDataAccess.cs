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

        public List<MVBCardModel> LoadCardPriceData<T, U>(string sqlStatement, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<MVBCardModel> rows = connection.Query<MVBCardModel, PricesModel, MVBCardModel>(sqlStatement, (card, price) => { card.AllPrices = price; return card; }).ToList();

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
