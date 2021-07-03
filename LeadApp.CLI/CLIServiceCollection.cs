using System;
using LeadApp.CLI.Processors;
using LeadApp.CLI.Processors.Interfaces;
using LeadApp.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LeadApp.CLI
{
    public static class CLIServiceCollection
    {
        public static void AddCLIServiceCollection(this IServiceCollection services)
        {
            DomainServiceCollection.AddDomainServiceCollection(services);
            services.AddScoped<IProcessor, LeadProcessor>();
        }
    }
}
