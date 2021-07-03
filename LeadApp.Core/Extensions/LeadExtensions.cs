using System;
using System.Collections.Generic;
using System.Text;
using LeadApp.Objects.Constants;
using LeadApp.Objects.Models;

namespace LeadApp.Core.Extensions
{
    public static class LeadExtensions
    {
        public static string ToDisplayText(this IList<Lead> leads)
        {
            StringBuilder sb = new();
            string headers = $"{Headers.LastName, -15} | {Headers.FirstName, -15} " +
                $"| {Headers.PropertyType, -15} | {Headers.Project, -15} " +
                $"| {Headers.StartDate, -15} | {Headers.Phone, -15}";
            sb.AppendLine(headers);

            foreach (Lead lead in leads)
            {
                string line = $"{lead.LastName,-15} | {lead.FirstName,-15} " +
                $"| {lead.PropertyType,-15} | {lead.Project,-15} " +
                $"| {lead.StartDate,-15} | {lead.PhoneNumber,-15}";
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
