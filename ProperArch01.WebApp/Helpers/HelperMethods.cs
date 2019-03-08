using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;

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
    }
}