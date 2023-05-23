using MVBToolsLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary;
using MVBToolsLibrary.Scrapers;
using MVBToolsLibrary.Repository;
using MVBToolsLibrary.Models.Provider_Models;

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
                    .ConfigureServices((_, services) =>
                    {

                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

                        services.Configure<DbSettings>(configuration.GetSection("ConnectionStrings"));


                        services.AddScoped<IEditionRepository<EditionModel>, EditionDbRepository>();
                        services.AddScoped<IEditionRepository<MVBEditionModel>, MvbApiEditionRespository>();
                        services.AddScoped<IEditionManager, EditionManager>();
                        services.AddScoped<ICardDbRepository<MVBCardModel>, CardDbRepository>();
                        services.AddScoped<ICardManager, CardManager>();
                        services.AddScoped<IPriceDbRepository, PriceDbRepository>();
                        services.AddScoped<IPriceManager, PriceManager>();
                        services.AddScoped<IMvbApiCardRepository, MvbApiCardRepository>();
                        services.AddScoped<IMvbApiPriceRepository, MvbApiPriceRepository>();
                        services.AddScoped<IScryfallApiPriceRepository, ScryfallApiPriceRepository>();
                        services.AddSingleton<IChromeDriverSetup, ChromeDriverSetup>();
                        services.AddSingleton<App>();
                        services.AddOptions();
                        services.AddHttpClient();
                    });
            }            
        }        
    }
}