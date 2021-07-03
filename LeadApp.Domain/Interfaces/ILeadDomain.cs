using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;

namespace LeadApp.Domain.Interfaces
{
    public interface ILeadDomain
    {
        IList<LeadDTO> GetDuplicates(List<LeadDTO> internalLeads, List<LeadDTO> externalLeads);
        Task<IList<LeadDTO>> GetExternalLeads();
        IList<LeadDTO> ParseFile(string file);
        LeadDTO ParseLead(string lead);
        IOrderedEnumerable<LeadDTO> SortBy(List<LeadDTO> leads, SortType sortType);
        IOrderedEnumerable<LeadDTO> ThenSortBy(IOrderedEnumerable<LeadDTO> leads, SortType[] sortType);
    }
}
