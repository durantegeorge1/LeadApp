using System;
using LeadApp.Domain.Interfaces;
using LeadApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LeadApp.Domain
{
    public static class DomainServiceCollection
    {
        public static void AddDomainServiceCollection(this IServiceCollection services)
        {
            ServicesServiceCollection.AddServicesServiceCollection(services);            
            services.AddScoped<ICommandLineDomain, CommandLineDomain>();
            services.AddScoped<ILeadDomain, LeadDomain>();
        }
    }
}
