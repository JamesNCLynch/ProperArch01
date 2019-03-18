using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProperArch01.Contracts.Models.Holiday
{
    public class CreateHolidayViewModel
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Holiday name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HolidayDate { get; set; }
    }
}