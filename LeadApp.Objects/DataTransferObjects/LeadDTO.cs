using System;
using LeadApp.Objects.Enums;

namespace LeadApp.Objects.DataTransferObjects
{
    public class LeadDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PropertyType PropertyType { get; set; }
        public DateTime StartDate { get; set; }
        public string Project { get; set; }
        public string Phone { get; set; }

        public bool Equals(LeadDTO compareDTO)
        {
            return compareDTO != null && Phone == compareDTO.Phone;
        }

        public override bool Equals(object obj)
        {
            return Equals((LeadDTO)obj);
        }

        public override int GetHashCode()
        {
            return Phone.GetHashCode();
        }
    }
}
