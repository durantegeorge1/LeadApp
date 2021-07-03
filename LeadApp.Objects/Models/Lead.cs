using System;
using LeadApp.Objects.DataTransferObjects;

namespace LeadApp.Objects.Models
{
    public class Lead
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PropertyType { get; set; }
        public string StartDate { get; set; }
        public string Project { get; set; }
        public string PhoneNumber { get; set; }
    }
}
