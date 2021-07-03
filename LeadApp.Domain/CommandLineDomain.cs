using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadApp.Domain.Interfaces;
using LeadApp.Objects.CommandLineArguments;
using LeadApp.Objects.Enums;
using LeadApp.Services.CommandLineService.CommandLineOptions;
using LeadApp.Services.CommandLineService.Interfaces;

namespace LeadApp.Domain
{
    public class CommandLineDomain : ICommandLineDomain
    {
        private readonly ICommandLineService commandLineService;

        public CommandLineDomain(ICommandLineService commandLineService)
        {
            this.commandLineService = commandLineService;
        }

        public LeadProcessorArguments GetLeadProcessorArguments(string[] args)
        {
            var options = commandLineService.ParseArugments<LeadProcessorOptions>(args);

            return new LeadProcessorArguments
            {
                FilePath = options.FilePath,
                PrimarySortType = (SortType)Enum.Parse(typeof(SortType), options.PrimarySortType),
                ExtendedSortTypeList = options.ExtendedSortTypeList.Select(s => (SortType)Enum.Parse(typeof(SortType), s)).ToArray()
            };
        }
    }
}
