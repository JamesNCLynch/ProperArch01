using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.Persistence.EntityModels
{
    public class ClassType : IEntity
    {
        [Key]
        public string Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Colours.Colour ClassColour { get; set; }
        public int Difficulty { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageFileName { get; set; }

        public virtual ICollection<ScheduledClass> ScheduledClass { get; set; }

        public virtual ICollection<ClassTimetable> ClassTimetable { get; set; }
    }
}