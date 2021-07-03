using System;
using System.Threading.Tasks;

namespace LeadApp.CLI.Processors.Interfaces
{
    public interface IProcessor
    {
        void Execute(string[] args);
    }
}
