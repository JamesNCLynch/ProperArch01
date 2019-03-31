using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.ClassAttendance
{
    public class EditClassAttendanceViewModel
    {
        public EditClassAttendanceViewModel()
        {

        }

        public EditClassAttendanceViewModel(ClassAttendanceDto dto)
        {
            Id = dto.Id;
            EnrolledDate = dto.EnrolledDate;
            EnrolledBy = dto.EnrolledBy;
            AttendeeId = dto.AttendeeId;
            AttendeeName = dto.AttendeeName;
            ScheduledClassId = dto.ScheduledClassId;
            ScheduledClassName = dto.ScheduledClassName;
            NoShow = dto.NoShow;
        }

        public string Id { get; set; }
        public DateTime EnrolledDate { get; set; }
        public string EnrolledBy { get; set; }
        public string AttendeeId { get; set; }
        public string AttendeeName { get; set; }
        public string ScheduledClassId { get; set; }
        public string ScheduledClassName { get; set; }
        public bool NoShow { get; set; }
    }
}