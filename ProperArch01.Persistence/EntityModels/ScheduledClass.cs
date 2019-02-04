using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProperArch01.Persistence.EntityModels
{
    public class ScheduledClass : IEntity
    {
        //[Key]
        public string ScheduledClassId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClassStartTime { get; set; }

        //public string ScheduledClassTypeId { get; set; }
        //[ForeignKey("ScheduledClassTypeId")]
        public virtual ClassType ClassType  { get; set; }
        
        public virtual ICollection<ClassAttendance> ClassAttendances { get; set; }

        //public string InstructorId { get; set; }
        //[ForeignKey("InstructorId")]
        public virtual GymUser Instructor { get; set; }
        public bool IsCancelled { get; set; }
    }
}