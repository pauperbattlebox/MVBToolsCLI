namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiPriceRepository
    {
        Task<decimal> Get(int id);
    }
}