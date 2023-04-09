using MVBToolsLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary.Interfaces;
using Microsoft.Extensions.Options;
using MVBToolsLibrary;

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

                        services.AddSingleton<IConfiguration>(configuration);
                        services.Configure<DbSettings>(configuration.GetSection("ConnectionStrings"));
                        services.AddScoped<IDbSettings>(c => c.GetService<IOptions<DbSettings>>().Value);
                        services.AddScoped<IEditionDbRepository<EditionModel>, EditionDbRepository>();
                        services.AddScoped<ICardDbRepository<MVBCardModel>, CardDbRepository>();
                        services.AddScoped<IPriceDbRepository, PriceDbRepository>();
                        services.AddScoped<IMvbApiCardRepository, MvbApiCardRepository>();
                        services.AddScoped<IMvbApiEditionRepository, MvbApiEditionRespository>();
                        services.AddScoped<IMvbApiPriceRepository, MvbApiPriceRepository>();
                        services.AddScoped<IScryfallApiPriceRepository, ScryfallApiPriceRepository>();
                        services.AddSingleton<App>();
                        services.AddOptions();
                        services.AddHttpClient();
                    });
            }            
        }        
    }
}