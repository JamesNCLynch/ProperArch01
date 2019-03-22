using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Models.Holiday;
using System.ComponentModel.DataAnnotations;

namespace ProperArch01.Contracts.Dto
{
    public class HolidayDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HolidayDate { get; set; }
    }
}