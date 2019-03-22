using ProperArch01.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProperArch01.Contracts.Models.ClassTimetable
{
    public class EditClassTimetableViewModel
    {
        public EditClassTimetableViewModel()
        {

        }

        //public EditClassTimetableViewModel(ClassTimetableDto dto) {
        //    Id = dto.Id;
        //    StartHour = dto.StartHour;
        //    StartMinutes = dto.StartMinutes;
        //    EndHour = dto.EndHour;
        //    EndMinutes = dto.EndMinutes;
        //    ClassTypeName = dto.ClassTypeName;
        //}

        public EditClassTimetableViewModel(ClassTimetableDto dto, IList<string> classTypeNames)
        {
            Id = dto.Id;
            StartHour = dto.StartHour;
            StartMinutes = dto.StartMinutes;
            EndHour = dto.EndHour;
            EndMinutes = dto.EndMinutes;
            ClassTypeName = dto.ClassTypeName;
            ClassTypeNames = classTypeNames;
            Weekday = dto.Weekday;
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "Class start hour")]
        public int StartHour { get; set; }
        [Required]
        [Display(Name = "Minutes")]
        public int StartMinutes { get; set; }
        [Required]
        [Display(Name = "Class end hour")]
        public int EndHour { get; set; }
        [Required]
        [Display(Name = "Minutes")]
        public int EndMinutes { get; set; }
        [Required]
        [Display(Name = "Class")]
        public IList<string> ClassTypeNames { get; set; }
        [Required]
        [Display(Name = "Class")]
        public string ClassTypeName { get; set; }
        [Required]
        [Display(Name = "Day")]
        public DayOfWeek Weekday { get; set; }

        public IList<SelectListItem> ClassTypeSelectList
        {
            get
            {
                return ClassTypeNames.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            }
            set { }
        }
    }
}