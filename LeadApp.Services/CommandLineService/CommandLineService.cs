using System;
using System.Threading.Tasks;
using CommandLine;
using LeadApp.Services.CommandLineService.Interfaces;

namespace LeadApp.Services.CommandLineService
{
    public class CommandLineService : ICommandLineService
    {
        public T ParseArugments<T>(string[] args)
        {
            Parsed<T> result = (Parsed<T>)Parser.Default.ParseArguments<T>(args);
            return result.Value;   
        }
    }
}
