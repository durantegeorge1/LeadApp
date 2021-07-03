using System;
using LeadApp.Objects.Enums;

namespace LeadApp.Objects.CommandLineArguments
{
    public class LeadProcessorArguments
    {
        public string FilePath { get; set; }
        public SortType PrimarySortType { get; set; }
        public SortType[] ExtendedSortTypeList { get; set; }
    }
}
