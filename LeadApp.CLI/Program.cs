using System;
using System.Threading.Tasks;
using CommandLine;
using LeadApp.CLI.Processors.Interfaces;
using LeadApp.Domain;
using LeadApp.Domain.Interfaces;
using LeadApp.Services.CommandLineService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeadApp.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            IProcessor leadProcessor = host.Services.GetService<IProcessor>();
            leadProcessor.Execute(args);
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddCLIServiceCollection());
    }
}
