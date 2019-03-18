using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.Holiday
{
    public class EditHolidayViewModel
    {
        public EditHolidayViewModel()
        {

        }

        public EditHolidayViewModel(HolidayDto dto)
        {
            Name = dto.Name;
            Id = dto.Id;
            HolidayDate = dto.HolidayDate;
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name ="Holiday name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HolidayDate { get; set; }
    }
}