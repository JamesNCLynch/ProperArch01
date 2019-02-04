using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProperArch01.Persistence.EntityModels
{
    public class ClassAttendance : IEntity
    {
        //[Key]
        public string ClassAttendanceId { get; set; }
        public DateTime EnrolledDate { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string EnrolledBy { get; set; }

        //public string AttendeeId { get; set; }

        //[ForeignKey("AttendeeId")]
        public virtual GymUser Attendee { get; set; }

        //public string ScheduledClassId { get; set; }

        //[ForeignKey("ScheduledClassId")]
        public virtual ScheduledClass ScheduledClass { get; set; }
        public bool NoShow { get; set; }
    }
}