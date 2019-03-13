using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProperArch01.Contracts.Models.ClassTimetable
{
    public class AddClassTimetableModel
    {
        public AddClassTimetableModel()
        {
            ClassTypeNames = new List<string>();
        }

        public AddClassTimetableModel(int weekday, int startHour)
        {
            Weekday = (DayOfWeek)weekday;
            StartHour = startHour;
            EndHour = startHour + 1;
            StartMinutes = 0;
            EndMinutes = 0;
            ClassTypeNames = new List<string>();
        }

        public AddClassTimetableModel(int weekday, int startHour, IList<string> classTypeNames)
        {
            Weekday = (DayOfWeek)weekday;
            StartHour = startHour;
            EndHour = startHour + 1;
            StartMinutes = 0;
            EndMinutes = 0;
            ClassTypeNames = classTypeNames;
        }

        [Required]
        [Display(Name = "Class start hour")]
        public int StartHour { get; set; }
        [Required]
        [Display(Name = "minutes")]
        public int StartMinutes { get; set; }
        [Required]
        [Display(Name = "Class end hour")]
        public int EndHour { get; set; }
        [Required]
        [Display(Name = "minutes")]
        public int EndMinutes { get; set; }
        [Required]
        [Display(Name = "Class")]
        public string ClassTypeName { get; set; }
        [Required]
        [Display(Name = "Class")]
        public IList<string> ClassTypeNames { get; set; }
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