using System;
using System.Collections.Generic;
using CommandLine;

namespace LeadApp.Services.CommandLineService.CommandLineOptions
{
    public class LeadProcessorOptions
    {
        [Option('f')]
        public string FilePath { get; set; }
        [Option('p')]
        public string PrimarySortType { get; set; }
        [Option('e', Separator = ',')]
        public IEnumerable<string> ExtendedSortTypeList { get; set; }
    }
}
