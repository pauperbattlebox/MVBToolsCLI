﻿using MVBToolsLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVBToolsLibrary.Repository.Api;
using MVBToolsLibrary.Repository.Db;
using MVBToolsLibrary;
using MVBToolsLibrary.Scrapers;
using MVBToolsLibrary.Repository;
using MVBToolsLibrary.Factories;

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
                        services.AddScoped<IEditionDbRepository<EditionModel>, EditionDbRepository>();
                        services.AddScoped<IEditionManager, EditionManager>();
                        services.AddScoped<ICardDbRepository<CardModel>, CardDbRepository>();
                        services.AddScoped<ICardManager, CardManager>();
                        services.AddScoped<IPriceDbRepository, PriceDbRepository>();
                        services.AddScoped<IPriceManager, PriceManager>();
                        services.AddScoped<IMvbApiCardRepository, MvbApiCardRepository>();
                        services.AddScoped<IMvbApiEditionRepository, MvbApiEditionRespository>();
                        services.AddScoped<IMvbApiPriceRepository, MvbApiPriceRepository>();
                        services.AddScoped<IScryfallApiPriceRepository, ScryfallApiPriceRepository>();
                        services.AddScoped<IDatabaseRepository, DatabaseRepository>();
                        services.AddSingleton<IChromeDriverSetup, ChromeDriverSetup>();
                        services.AddSingleton<App>();
                        services.AddOptions();
                        services.AddHttpClient();
                    });
            }            
        }        
    }
}