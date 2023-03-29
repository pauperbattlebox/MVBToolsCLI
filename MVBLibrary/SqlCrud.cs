using DataAccessLibrary.Models;

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

        public async Task<IEnumerable<EditionModel>> GetAllEditions()
        {
            string sql = @"SELECT * FROM dbo.Edition;";

            return await db.LoadAsyncData<EditionModel, dynamic>(sql, new  { }, _connectionString);
        }

        public async Task<EditionModel> GetEdition(int csId)
        {
            string sql = @"SELECT CsId, Name
                            FROM dbo.Edition
                            WHERE CsId = @CsId;";

            return await db.LoadAsyncSingleData<EditionModel, dynamic>(sql, new { CsId = csId }, _connectionString);

        }

        public async Task<MVBCardModel> GetCard(int csId)
        {
            string sql = @"SELECT CsId, Name, MtgJsonCode
                            FROM dbo.Card
                            WHERE CsId = @CsId;";

            return await db.LoadAsyncSingleData<MVBCardModel, dynamic>(sql, new { CsId = csId }, _connectionString);

        }

        public async Task<IEnumerable<MVBCardModel>> GetAllCardsByEditionId(string mtgJsonCode)
        {
            string sql = @"SELECT c.Name, c.MtgJsonCode, e.csName
                            FROM dbo.Card as c
                            LEFT JOIN dbo.Edition as e
                            ON c.MtgJsonCode = e.MtgJsonCode
                            WHERE e.MtgJsonCode = @MtgJsonCode;";

            return await db.LoadAsyncData<MVBCardModel, dynamic>(sql, new { MtgJsonCode = mtgJsonCode }, _connectionString);

        }



        public async Task<IEnumerable<DbCardModel>> GetCardPrice(int csId)
        {
            string sql = @"SELECT c.CsId, c.Name, 0 as splitter, p.CsPrice, p.ScryfallPrice
                            FROM dbo.Card as c
                            LEFT JOIN dbo.Prices AS p ON c.CsId = p.CsId
                            WHERE c.CsId = @CsId;";

            return db.LoadCardPriceData<DbCardModel, dynamic>(sql, new { CsId = csId }, _connectionString);
        }       

        public async Task<EditionModel> CreateSet(EditionModel edition)
        {
            string sql = @"INSERT dbo.Edition (CsId, CsName, MtgJsonCode)
                            VALUES (@CsId, @CsName, @MtgJsonCode);";
            db.SaveData(sql,
                new { edition.CsId, edition.CsName, edition.MtgJsonCode },
                _connectionString);

            return edition;
        }

        public void CreateCard(MVBCardModel card)
        {
            string sql = @"IF NOT EXISTS
                            (SELECT CsId FROM dbo.Card WHERE CsId = @CsId)
                            BEGIN
                            INSERT INTO dbo.Card (CsId, Name, MtgjsonId, ScryfallId, MtgJsonCode)
                            VALUES (@CsId, @Name, @MtgJsonId, @ScryfallId, @MtgJsonCode)
                            END;";
            db.SaveData(sql,
                new { card.CsId, card.Name, card.MtgJsonId, card.ScryfallId, card.MtgJsonCode },
                _connectionString);
        }

        public async Task UpdateMvbPrice(int csId, decimal price)
        {            
            string sql = @"IF NOT EXISTS
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

            db.SaveData(sql,
                new { CsPrice = price, CsId = csId },
                _connectionString);
        }

        public async Task UpdateScryfallPrice(string scryfallid, decimal price)
        {
            string sqlGetCsId = @"SELECT CsId
                                    FROM dbo.Card
                                    WHERE ScryfallId = @ScryfallId;";

            MVBCardModel output = new MVBCardModel();

            output = db.LoadData<MVBCardModel, dynamic>(sqlGetCsId, new { ScryfallId = scryfallid }, _connectionString).FirstOrDefault();

            string sql = @"IF NOT EXISTS
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

            db.SaveData(sql,
                new { ScryfallPrice = price, CsId = output.CsId },
                _connectionString);
        }
    }
}