using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string _connectionString;
        private SqlDataAccess db = new SqlDataAccess();

        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SetsModel> GetAllSets()
        {
            string sql = "select * from dbo.Sets;";

            return db.LoadData<SetsModel, dynamic>(sql, new  { }, _connectionString);
        }

        public SetCardsModel GetSetsAndCardsInSet(int id)
        {
            string sql = "select * from dbo.Sets where Id = @Id;";

            SetCardsModel output = new SetCardsModel();

            output.Sets = db.LoadData<SetsModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

            sql = @"select c.*
                    from dbo.Cards c
                    inner join dbo.CardsSets cs on cs.CardId = c.Id
                    where cs.SetId = @Id;";

            output.Cards = db.LoadData<CardsModel, dynamic>(sql, new { Id = id }, _connectionString);

            return output;
        }

        public void CreateSet(SetsModel set)
        {
            string sql = "insert into dbo.Sets (CsId, CsName, MtgjsonCode, CreatedOn) values (@CsId, @CsName, @MtgjsonCode, @Createdon);";
            db.SaveData(sql,
                new { set.CsId, set.CsName, set.MtgjsonCode, set.CreatedOn },
                _connectionString);

            Console.WriteLine($"{set.CsName} added!");
        }
    }
}
