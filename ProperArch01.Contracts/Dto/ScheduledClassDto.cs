using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Dto
{
    public class ScheduledClassDto
    {
        public string Id { get; set; }
        public DateTime ClassStartTime { get; set; }
        public string ClassTypeName { get; set; }
        public string InstructorName { get; set; }
        public bool IsCancelled { get; set; }
    }
}