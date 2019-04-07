using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Dto;

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
            IEnumerable<int> hours = Enumerable.Range(7, 15);

            return hours.ToList();
        }
    }
}