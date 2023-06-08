using MVBToolsLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVBToolsLibrary;
using MVBToolsLibrary.Scrapers;
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
                    .ConfigureServices((_, services) =>
                    {

                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

                        services.Configure<DbSettings>(configuration.GetSection("ConnectionStrings"));
                        services.AddScoped<IScryfallApiRepository, ScryfallApiRepository>();
                        services.AddScoped<IMvbApiCardRepository, MvbApiCardRepository>();
                        services.AddScoped<IObjectManagerRepository, ManagerRepository>();
                        services.AddScoped<IEditionManager, EditionManager>();
                        services.AddScoped<ICardManager, CardManager>();
                        services.AddScoped<IPriceManager, PriceManager>();
                        services.AddSingleton<IChromeDriverSetup, ChromeDriverSetup>();
                        services.AddSingleton<App>();
                        services.AddOptions();
                        services.AddHttpClient();
                    });
            }            
        }        
    }
}