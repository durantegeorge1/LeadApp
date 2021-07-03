using System;
using System.IO;
using System.Collections.Generic;
using LeadApp.Objects.DataTransferObjects;
using System.Threading.Tasks;
using LeadApp.Objects.Enums;
using LeadApp.Domain.Interfaces;
using LeadApp.Services.FileService.Interfaces;
using System.Linq;
using LeadApp.Services.HttpClientService.Interfaces;
using Microsoft.Extensions.Options;
using LeadApp.Objects;
using System.Net.Http;
using Newtonsoft.Json;

namespace LeadApp.Domain
{
    public class LeadDomain : ILeadDomain
    {
        private const string APIHeaderKey = "X-Api-Key";
        private readonly AppSettings appSettings;
        private readonly IFileService fileService;
        private readonly IHttpClientService httpClientService;

        public LeadDomain(IOptions<AppSettings> options, IFileService fileService, IHttpClientService httpClientService)
        {
            appSettings = options.Value;
            this.fileService = fileService;
            this.httpClientService = httpClientService;
        }

        public IList<LeadDTO> GetDuplicates(List<LeadDTO> internalLeads, List<LeadDTO> externalLeads)
        {
            return internalLeads.Intersect(externalLeads).ToList();
        }

        public async Task<IList<LeadDTO>> GetExternalLeads()
        {
            HttpRequestMessage request = new(HttpMethod.Get, appSettings.API);
            request.Headers.Add(APIHeaderKey, appSettings.APIKey);
            HttpResponseMessage response = await httpClientService.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LeadDTO>>(responseString);
            }
            else
            {
                return new List<LeadDTO>();
            }
        }

        public IList<LeadDTO> ParseFile(string file)
        {
            List<LeadDTO> leads = new();
            using StreamReader streamReader = fileService.StreamReader(file);
            string lead;
            while((lead = streamReader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(lead))
                {
                    continue;
                }
                leads.Add(ParseLead(lead));
            }
            return leads;
        }

        public LeadDTO ParseLead(string lead)
        {
            string[] leadProperties = GetLeadProperties(lead);
            return new LeadDTO
            {
                LastName = leadProperties[0].Trim(),
                FirstName = leadProperties[1].Trim(),
                PropertyType = (PropertyType)Enum.Parse(typeof(PropertyType), leadProperties[2].Trim()),
                Project = leadProperties[3].Trim(),
                StartDate = DateTime.Parse(leadProperties[4].Trim()),
                Phone = leadProperties[5].Trim()
            };
        }

        public IOrderedEnumerable<LeadDTO> SortBy(List<LeadDTO> leads, SortType sortType)
        {
            return sortType switch
            {
                SortType.LastNameAsc => leads.OrderBy(l => l.LastName),
                SortType.LastNameDesc => leads.OrderByDescending(l => l.LastName),
                SortType.PropertyTypeAsc => leads.OrderBy(l => Enum.GetName(typeof(PropertyType), l.PropertyType)),
                SortType.PropertyTypeDesc => leads.OrderByDescending(l => Enum.GetName(typeof(PropertyType), l.PropertyType)),
                SortType.ProjectAsc => leads.OrderBy(l => l.Project),
                SortType.ProjectDesc => leads.OrderByDescending(l => l.Project),
                SortType.StartDateAsc => leads.OrderBy(l => l.StartDate),
                SortType.StartDateDesc => leads.OrderByDescending(l => l.StartDate),
                _ => throw new ArgumentException($"Sorting for sortType: {sortType} is not implemented.")
            };
        }

        public IOrderedEnumerable<LeadDTO> ThenSortBy(IOrderedEnumerable<LeadDTO> leads, SortType[] sortTypes)
        {
            foreach (SortType sortType in sortTypes)
            {
                leads = sortType switch
                {
                    SortType.LastNameAsc => leads.ThenBy(l => l.LastName),
                    SortType.LastNameDesc => leads.ThenByDescending(l => l.LastName),
                    SortType.PropertyTypeAsc => leads.ThenBy(l => Enum.GetName(typeof(PropertyType), l.PropertyType)),
                    SortType.PropertyTypeDesc => leads.ThenByDescending(l => Enum.GetName(typeof(PropertyType), l.PropertyType)),
                    SortType.ProjectAsc => leads.ThenBy(l => l.Project),
                    SortType.ProjectDesc => leads.ThenByDescending(l => l.Project),
                    SortType.StartDateAsc => leads.ThenBy(l => l.StartDate),
                    SortType.StartDateDesc => leads.ThenByDescending(l => l.StartDate),
                    _ => throw new ArgumentException($"Sorting for sortType: {sortType} is not implemented.")
                };
            }
            return leads;
        }

        private static string[] GetLeadProperties(string lead)
        {
            char delimiter;
            if(lead.Split('|').Length == 6)
            {
                delimiter = '|';
            }
            else if(lead.Split(',').Length == 6)
            {
                delimiter = ',';
            }
            else if(lead.Split(' ').Length == 6)
            {
                delimiter = ' ';
            }
            else
            {
                throw new ArgumentException("Input file not formated correctly");
            }
            return lead.Split(delimiter);
        }
    }
}
