using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProperArch01.Persistence.EntityModels
{
    public class Holidays : IEntity
    {
        //[Key]
        public string HolidaysId { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}