﻿using DataAccessLibrary.Models;

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

        public MVBCardModel GetCard(int csId)
        {
            string sql = "SELECT CsId, Name FROM dbo.Card WHERE CsId = @CsId;";

            return db.LoadData<MVBCardModel, dynamic>(sql, new { CsId = csId }, _connectionString).FirstOrDefault();

        }

        public DbCardModel GetCardPrice(int csId)
        {
            string sql = "SELECT c.CsId, c.Name, 0 as splitter, p.CsPrice, p.ScryfallPrice FROM dbo.Card as c LEFT JOIN dbo.Prices AS p ON c.CsId = p.CsId WHERE c.CsId = @CsId;";

            return db.LoadCardPriceData<DbCardModel, dynamic>(sql, new { CsId = csId }, _connectionString).FirstOrDefault();
        }

        public EditionCardsModel GetCardsByEdition(int id)
        {
            string sql = "select * from dbo.Edition where Id = @Id;";

            EditionCardsModel output = new EditionCardsModel();

            output.Edition = db.LoadData<EditionModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

            sql = @"select c.*
                    from dbo.Card c
                    inner join dbo.CardsSets cs on cs.CardId = c.Id
                    where cs.SetId = @Id;";

            output.Cards = db.LoadData<MVBCardModel, dynamic>(sql, new { Id = id }, _connectionString);

            return output;
        }

        public void CreateSet(EditionModel edition)
        {
            string sql = "insert into dbo.Edition (CsId, CsName, MtgJsonCode) values (@CsId, @CsName, @MtgJsonCode);";
            db.SaveData(sql,
                new { edition.CsId, edition.CsName, edition.MtgJsonCode },
                _connectionString);
        }

        public void AddCard(MVBCardModel card)
        {
            string sql = "IF NOT EXISTS (SELECT CsId FROM dbo.Card WHERE CsId = @CsId) BEGIN insert into dbo.Card (CsId, Name, MtgjsonId, ScryfallId, MtgJsonCode) values (@CsId, @Name, @MtgJsonId, @ScryfallId, @MtgJsonCode) END;";
            db.SaveData(sql,
                new { card.CsId, card.Name, card.MtgJsonId, card.ScryfallId, card.MtgJsonCode },
                _connectionString);
        }

        public void UpdateMvbPrice(int csId, decimal price)
        {            
            string sql = "IF NOT EXISTS (SELECT CsId FROM dbo.Prices WHERE CsId = @CsId) BEGIN INSERT INTO dbo.Prices (CsId, CsPrice) values (@CsId, @CsPrice) END ELSE BEGIN UPDATE dbo.Prices SET CsPrice = @CsPrice WHERE CsId = @CsId END;";

            db.SaveData(sql,
                new { CsPrice = price, CsId = csId },
                _connectionString);
        }

        public void UpdateScryfallPrice(string scryfallid, decimal price)
        {
            string sqlGetCsId = "select CsId from dbo.Card where ScryfallId = @ScryfallId;";

            MVBCardModel output = new MVBCardModel();

            output = db.LoadData<MVBCardModel, dynamic>(sqlGetCsId, new { ScryfallId = scryfallid }, _connectionString).FirstOrDefault();

            string sql = "IF NOT EXISTS (SELECT CsId FROM dbo.Prices WHERE CsId = @CsId) BEGIN INSERT INTO dbo.Prices (CsId, ScryfallPrice) values (@CsId, @ScryfallPrice) END ELSE BEGIN UPDATE dbo.Prices SET ScryfallPrice = @ScryfallPrice WHERE CsId = @CsId END;";

            db.SaveData(sql,
                new { ScryfallPrice = price, CsId = output.CsId },
                _connectionString);
        }
    }
}