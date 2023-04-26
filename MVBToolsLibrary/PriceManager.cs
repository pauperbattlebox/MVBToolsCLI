using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Db;

namespace MVBToolsLibrary
{
    public class PriceManager : IPriceManager
    {
        private readonly IPriceDbRepository _priceDbRepository;

        public PriceManager(IPriceDbRepository priceDbRepository)
        {
            _priceDbRepository = priceDbRepository;
        }

        public async Task<DbCardModel> GetPriceFromDb(int id)
        {
            return await _priceDbRepository.Get(id);
        }        
    }
}
