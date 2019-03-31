using ProperArch01.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.ClassAttendance
{
    public class CreateClassAttendanceViewModel
    {
        public CreateClassAttendanceViewModel()
        {

        }

        public CreateClassAttendanceViewModel(ScheduledClassDto dto)
        {
            ScheduledClassId = dto.Id;
            ScheduledClassName = dto.ClassTypeName;
            ClassStartTime = dto.ClassStartTime;
        }

        public DateTime EnrolledDate { get; set; }
        public string EnrolledBy { get; set; }

        public string AttendeeId { get; set; }

        public string AttendeeName { get; set; }

        public string ScheduledClassId { get; set; }
        public string ScheduledClassName { get; set; }
        public DateTime ClassStartTime { get; set; }
    }
}