using System;
using LeadApp.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LeadApp.API
{
    public static class APIServiceCollection
    {
        public static void AddAPIServiceCollection(this IServiceCollection services)
        {
            DomainServiceCollection.AddDomainServiceCollection(services);
        }
    }
}
