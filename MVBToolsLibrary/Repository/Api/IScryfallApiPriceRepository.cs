namespace MVBToolsLibrary.Repository.Api
{
    public interface IScryfallApiPriceRepository
    {
        Task<decimal> Get(string id);
    }
}