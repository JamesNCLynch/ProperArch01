using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProperArch01.Persistence.EntityModels
{
    public class ClassType : IEntity
    {
        //[Key]
        public string ClassTypeId { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Colour ClassColour { get; set; }
        public enum Colour
        {
            Aqua,
            DeepPink,
            DeepSkyBlue,
            MediumOrchid,
            Lime,
            MidnightBlue,
            Navy,
            Plum,
            RebeccaPurple,
            Red,
            Salmon,
            SkyBlue,
            Thistle,
            Violet,
            YellowGreen
        }
        public int Difficulty { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<ScheduledClass> ScheduledClass { get; set; }

        public virtual ICollection<ClassTimetable> ClassTimetable { get; set; }
    }
}