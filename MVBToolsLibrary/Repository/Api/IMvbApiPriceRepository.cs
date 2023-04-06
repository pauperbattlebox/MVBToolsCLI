namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiPriceRepository
    {
        Task<decimal> Get(int id, Func<int, string, string> buildUrl);
    }
}