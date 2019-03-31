using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.ClassAttendance
{
    public class ClassAttendanceIndexViewModel
    {
        public IList<ScheduledClassDto> UpcomingScheduledClasses { get; set; }
        public IList<ClassAttendanceDto> ClassesCurrentlySignedUp { get; set; }
        public IList<ClassAttendanceDto> PastClassesAttended { get; set; }
    }
}
