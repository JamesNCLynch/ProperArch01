using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.ScheduledClass
{
    public class DetailedScheduledClassViewModel
    {
        public DetailedScheduledClassViewModel()
        {

        }
        //public DetailedScheduledClassViewModel(ScheduledClassDto dto, List<string> attendeeNames)
        //{
        //    Id = dto.Id;
        //    ClassStartTime = dto.ClassStartTime;
        //    ClassTypeName = dto.ClassTypeName;
        //    InstructorName = dto.InstructorName;
        //    IsCancelled = dto.IsCancelled;
        //    AttendeeNames = attendeeNames;
        //}

        public string Id { get; set; }
        public DateTime ClassStartTime { get; set; }
        public string ClassTypeName { get; set; }
        public string InstructorName { get; set; }
        public bool IsCancelled { get; set; }
        public List<ClassAttendanceDto> Attendances { get; set; }
    }
}