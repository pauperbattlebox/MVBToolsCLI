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

        public EditionManager(IEditionDbRepository<EditionModel> dbRepository)
        {
            _dbRepository = dbRepository;            
        }

        public async Task<IEnumerable<EditionModel>> GetAllEditionsFromDb()
        {
            return await _dbRepository.GetAll();
        }        
    }
}
