using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadApp.CLI.Processors.Interfaces;
using LeadApp.Core.Extensions;
using LeadApp.Core.Mappers;
using LeadApp.Domain.Interfaces;
using LeadApp.Objects.DataTransferObjects;

namespace LeadApp.CLI.Processors
{
    public class LeadProcessor : IProcessor
    {
        private readonly ILeadDomain leadDomain;
        private readonly ICommandLineDomain commandLineDomain;

        public LeadProcessor(ILeadDomain leadDomain, ICommandLineDomain commandLineDomain)
        {
            this.leadDomain = leadDomain;
            this.commandLineDomain = commandLineDomain;
        }

        public void Execute(string[] args)
        {
            var options = commandLineDomain.GetLeadProcessorArguments(args);
            List<LeadDTO> leads = (List<LeadDTO>)leadDomain.ParseFile(options.FilePath);
            IOrderedEnumerable<LeadDTO> sortedLeads = leadDomain.SortBy(leads, options.PrimarySortType);
            if (options.ExtendedSortTypeList.Length > 0)
            {
                leads = leadDomain.ThenSortBy(sortedLeads, options.ExtendedSortTypeList).ToList();
            }
            else
            {
                leads = sortedLeads.ToList();
            }

            string displayText = leads.ToLeadMap().ToDisplayText();
            Console.WriteLine(displayText);
        }
    }
}
