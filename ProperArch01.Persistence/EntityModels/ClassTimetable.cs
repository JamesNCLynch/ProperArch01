using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProperArch01.Persistence.EntityModels
{
    public class ClassTimetable : IEntity
    {
        [Key]
        public string Id { get; set; }

        public string ClassTypeId { get; set; }
        [ForeignKey("ClassTypeId")]
        public ClassType ClassType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek Weekday { get; set; }
    }
}