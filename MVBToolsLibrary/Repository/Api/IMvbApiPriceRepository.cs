namespace MVBToolsLibrary.Repository.Api
{
    public interface IMvbApiPriceRepository
    {
        public Task<decimal> Get(int id);
    }
}