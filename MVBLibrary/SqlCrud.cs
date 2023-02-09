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

        public List<EditionModel> GetAllEditions()
        {
            string sql = "select * from dbo.Edition;";

            return db.LoadData<EditionModel, dynamic>(sql, new  { }, _connectionString);
        }

        public EditionCardsModel GetSetsAndCardsInSet(int id)
        {
            string sql = "select * from dbo.Edition where Id = @Id;";

            EditionCardsModel output = new EditionCardsModel();

            output.Edition = db.LoadData<EditionModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

            sql = @"select c.*
                    from dbo.Card c
                    inner join dbo.CardsSets cs on cs.CardId = c.Id
                    where cs.SetId = @Id;";

            output.Cards = db.LoadData<CardModel, dynamic>(sql, new { Id = id }, _connectionString);

            return output;
        }

        public void CreateSet(EditionModel edition)
        {
            string sql = "insert into dbo.Edition (CsId, CsName, MtgJsonCode) values (@CsId, @CsName, @MtgJsonCode);";
            db.SaveData(sql,
                new { edition.CsId, edition.CsName, edition.MtgJsonCode },
                _connectionString);

            Console.WriteLine($"{edition.CsName} added!");
        }
    }
}
