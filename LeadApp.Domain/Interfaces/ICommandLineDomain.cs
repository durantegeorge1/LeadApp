using System;
using LeadApp.Objects.CommandLineArguments;

namespace LeadApp.Domain.Interfaces
{
    public interface ICommandLineDomain
    {
        LeadProcessorArguments GetLeadProcessorArguments(string[] args);
    }
}
