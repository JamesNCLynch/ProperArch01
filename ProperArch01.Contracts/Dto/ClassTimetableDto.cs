using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Dto
{
    public class ClassTimetableDto
    {
        public string Id { get; set; }
        public string ClassTypeName { get; set; }
        public int StartHour { get; set; }
        public int StartMinutes { get; set; }
        public int EndHour { get; set; }
        public int EndMinutes { get; set; }
        public DayOfWeek Weekday { get; set; }
    }
}