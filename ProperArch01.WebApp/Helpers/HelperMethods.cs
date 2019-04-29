using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Dto;
using System.Configuration;

namespace ProperArch01.WebApp.Helpers
{
    public static class HelperMethods
    {
        public static List<string> GetAllRoleNames()
        {
            var roles = new List<string>
            {
                RoleNames.AdminName,
                RoleNames.AttendeeName,
                RoleNames.InstructorName
            };

            return roles;
        }

        public static List<int> GetSlotMinutes ()
        {
            return new List<int> {0, 15, 30, 45};
        }

        public static List<int> GetSlotHours()
        {
            var earliestSlotStartHour = Int32.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]);
            var latestSlotEndHour = Int32.Parse(ConfigurationManager.AppSettings["GymClosingHour"]);

            IEnumerable<int> hours = Enumerable.Range(earliestSlotStartHour, latestSlotEndHour - earliestSlotStartHour);

            return hours.ToList();
        }
    }
}