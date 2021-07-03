using System;
using System.Threading.Tasks;

namespace LeadApp.Services.CommandLineService.Interfaces
{
    public interface ICommandLineService
    {
        T ParseArugments<T>(string[] args);
    }
}
