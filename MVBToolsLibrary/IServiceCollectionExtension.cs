using Microsoft.Extensions.DependencyInjection;
using MVBToolsLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary
{
    public class IServiceCollectionExtension
    {
        public static IServiceCollection AddLibraryServices(IServiceCollection services)
        {
            services.AddScoped<IMvbApiRepository, MvbApiRepository>();
            services.AddHttpClient<IMvbApiRepository, MvbApiRepository>(client =>
            {
                client.BaseAddress = new Uri("https://www.multiversebridge.com/api/v1/");
            });

            return services;
        }
    }
}
