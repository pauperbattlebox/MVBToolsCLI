using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVBToolsLibrary;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Json;
using MVBToolsLibrary.Repository;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel;
using System.Diagnostics.Tracing;

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
                        services.AddScoped<IRepository<EditionModel, int>, EditionRepository>();
                        services.AddSingleton<App>();
                    });
            }
        }        
    }
}