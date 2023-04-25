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
        private readonly IMvbApiEditionRepository _mvbApiEditionRepository;

        public EditionManager(IEditionDbRepository<EditionModel> dbRepository, IMvbApiEditionRepository mvbApiEditionRepository)
        {
            _dbRepository = dbRepository;
            _mvbApiEditionRepository = mvbApiEditionRepository;
        }

        public async Task<IEnumerable<EditionModel>> GetAllEditionsFromDb()
        {
            return await _dbRepository.GetAll();
        }        
    }
}
