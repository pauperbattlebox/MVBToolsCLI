using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVBToolsLibrary;
using MVBToolsLibrary.Repository;

namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            using IHost host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
                        
            services.GetRequiredService<App>().Run(args);


            static IHostBuilder CreateHostBuilder(string[] args)
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((services) =>
                    {
                        services.AddScoped<IEditionDbRepository<EditionModel>, EditionDbRepository>();
                        services.AddScoped<ICardDbRepository<MVBCardModel>, CardDbRepository>();
                        services.AddSingleton<App>();
                    });
            }
        }
    }
}