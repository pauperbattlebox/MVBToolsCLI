using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Models;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public class EditionManager : IEditionManager
    {
        private readonly IEditionDbRepository<EditionModel> _dbRepository;
        private readonly IMvbApiEditionRepository _mvbApiRepository;

        public EditionManager(IEditionDbRepository<EditionModel> dbRepository, IMvbApiEditionRepository mvbApiRepository)
        {
            _dbRepository = dbRepository;
            _mvbApiRepository= mvbApiRepository;
        }

        public async Task<IEnumerable<EditionModel>> GetAllEditionsFromDb()
        {
            return await _dbRepository.GetAll();
        }

        public async Task AddEditionToDb(int editionId)
        {
            var editionToAdd = GetEditionFromApi(editionId).Result;

            await _dbRepository.Insert(editionToAdd);
        }

        private async Task<EditionModel> GetEditionFromApi(int editionId)
        {
            return await _mvbApiRepository.Get(editionId);            
        }
    }
}
