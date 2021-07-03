using System;
using LeadApp.Services.CommandLineService.Interfaces;
using LeadApp.Services.FileService.Interfaces;
using LeadApp.Services.HttpClientService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LeadApp.Services
{
    public static class ServicesServiceCollection
    {
        public static void AddServicesServiceCollection(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IHttpClientService, HttpClientService.HttpClientService>();
            services.AddScoped<IFileService, FileService.FileService>();
            services.AddScoped<ICommandLineService, CommandLineService.CommandLineService>();
        }
    }
}
