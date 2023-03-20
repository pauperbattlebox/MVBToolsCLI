using DataAccessLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection().AddSingleton<IRepository<EditionModel, int>, EditionRepository>()
                                                    .BuildServiceProvider();

            return provider;
        }
    }
}
