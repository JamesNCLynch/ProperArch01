using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Dto
{
    public class ClassAttendanceDto
    {
        public string Id { get; set; }
        public DateTime EnrolledDate { get; set; }
        public string EnrolledBy { get; set; }
        public string AttendeeId { get; set; }        
        public string AttendeeName { get; set; }
        public string ScheduledClassId { get; set; }
        public string ScheduledClassName { get; set; }
        public DateTime ClassStartDateTime { get; set; }
        public bool NoShow { get; set; }
    }
}