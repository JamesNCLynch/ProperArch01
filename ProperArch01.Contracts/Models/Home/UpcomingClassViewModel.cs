using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.Home
{
    public class UpcomingClassViewModel
    {
        public string ClassTypeName { get; set; }
        public string ClassTypeId { get; set; }
        public string NextScheduledClassStartTime { get; set; }
    }
}