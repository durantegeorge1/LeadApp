using System;
using System.Collections.Generic;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using LeadApp.Objects.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadApp.Tests
{
    public static class MockData
    {
        public static List<Lead> Leads => new()
        {
            new Lead
            {
                FirstName = "John",
                LastName = "Doe",
                PropertyType = "House",
                Project = "Paint",
                StartDate = "7/1/2021",
                PhoneNumber = "+14045551234"
            },
            new Lead
            {
                FirstName = "John",
                LastName = "Jackson",
                PropertyType = "Condo",
                Project = "Roof",
                StartDate = "7/2/2021",
                PhoneNumber = "+14045551235"
            },
            new Lead
            {
                FirstName = "John",
                LastName = "Smith",
                PropertyType = "Trailer",
                Project = "Plumbing",
                StartDate = "7/3/2021",
                PhoneNumber = "+16785551234"
            },
            new Lead
            {
                FirstName = "John",
                LastName = "Banks",
                PropertyType = "House",
                Project = "Lawncare",
                StartDate = "7/4/2021",
                PhoneNumber = "+17705551234"
            }
        };

        public static List<LeadDTO> InternalLeads => new()
        {
            new LeadDTO
            {
                FirstName = "John",
                LastName = "Doe",
                PropertyType = PropertyType.House,
                Project = "Paint",
                StartDate = DateTime.Parse("2021-07-01"),
                Phone = "+14045551234"
            },
            new LeadDTO
            {
                FirstName = "John",
                LastName = "Jackson",
                PropertyType = PropertyType.Condo,
                Project = "Roof",
                StartDate = DateTime.Parse("2021-07-02"),
                Phone = "+14045551235"
            },
            new LeadDTO
            {
                FirstName = "John",
                LastName = "Smith",
                PropertyType = PropertyType.Trailer,
                Project = "Plumbing",
                StartDate = DateTime.Parse("2021-07-03"),
                Phone = "+16785551234"
            },
            new LeadDTO
            {
                FirstName = "John",
                LastName = "Banks",
                PropertyType = PropertyType.House,
                Project = "Lawncare",
                StartDate = DateTime.Parse("2021-07-04"),
                Phone = "+17705551234"
            }
        };

        public static List<LeadDTO> ExternalLeads => new()
        {
            new LeadDTO
            {
                FirstName = "John",
                LastName = "Doe",
                PropertyType = PropertyType.House,
                Project = "Paint",
                StartDate = DateTime.Parse("2021-07-01"),
                Phone = "+14045551234"
            },
            new LeadDTO
            {
                FirstName = "John",
                LastName = "Jackson",
                PropertyType = PropertyType.Condo,
                Project = "Roof",
                StartDate = DateTime.Parse("2021-07-02"),
                Phone = "+14045551235"
            },
            new LeadDTO
            {
                FirstName = "Jane",
                LastName = "Smith",
                PropertyType = PropertyType.House,
                Project = "Paint",
                StartDate = DateTime.Parse("2021-07-01"),
                Phone = "+14045551234"
            },
            new LeadDTO
            {
                FirstName = "Jane",
                LastName = "Banks",
                PropertyType = PropertyType.House,
                Project = "Paint",
                StartDate = DateTime.Parse("2021-07-01"),
                Phone = "+14045551234"
            }
        };
    }
}
