using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Models.Colour;

namespace ProperArch01.Contracts.Models.ClassTimetable
{
    public class ClassTimetableSlotViewModel
    {
        public string Id { get; set; }
        public string ClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek Day  { get; set; }
        public RGBAModel Colour { get; set; }
    }
}