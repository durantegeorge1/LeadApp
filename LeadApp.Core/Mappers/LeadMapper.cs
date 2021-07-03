using System;
using System.Collections.Generic;
using System.Linq;
using LeadApp.Core.Extensions;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using LeadApp.Objects.Models;

namespace LeadApp.Core.Mappers
{
    public static class LeadMapper
    {
        public static LeadDTO ToLeadDTOMap(this Lead lead)
        {
            return new LeadDTO
            {
                FirstName = lead.FirstName,
                LastName = lead.LastName,
                PropertyType = (PropertyType)Enum.Parse(typeof(PropertyType), lead.PropertyType),
                Project = lead.Project,
                StartDate = DateTime.Parse(lead.StartDate),
                Phone = lead.PhoneNumber,
            };
        }

        public static IList<LeadDTO> ToLeadDTOMap(this List<Lead> leads)
        {
            return leads.Select(lead => lead.ToLeadDTOMap()).ToList();
        }

        public static Lead ToLeadMap(this LeadDTO leadDTO)
        {
            return new Lead
            {
                FirstName = leadDTO.FirstName,
                LastName = leadDTO.LastName,
                PropertyType = Enum.GetName(typeof(PropertyType), leadDTO.PropertyType),
                Project = leadDTO.Project,
                StartDate = leadDTO.StartDate.ToString("M/d/yyyy"),
                PhoneNumber = leadDTO.Phone.ToPhoneNumber()
            };
        }

        public static IList<Lead> ToLeadMap(this IList<LeadDTO> leadDTOs)
        {
            return leadDTOs.Select(leadDTO => leadDTO.ToLeadMap()).ToList();
        }
    }
}
