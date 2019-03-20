using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.ScheduledClass
{
    public class ScheduledClassIndexViewModel
    {
        public IEnumerable<ScheduledClassDto> ScheduledClassesRequiringCompletion { get; set; }
        public IEnumerable<ScheduledClassDto> ScheduledClassesCompleted { get; set; }
        public IEnumerable<ScheduledClassDto> CancelledScheduledClasses { get; set; }
    }
}