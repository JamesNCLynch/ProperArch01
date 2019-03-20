using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProperArch01.Contracts.Models.ScheduledClass
{
    public class CreateScheduledClassViewModel
    {
        public CreateScheduledClassViewModel()
        {

        }
        public CreateScheduledClassViewModel(string className, DateTime startTime, List<string> instructorNames)
        {
            ClassTypeName = className;
            ClassStartTime = startTime;
            InstructorNames = instructorNames;
        }
        public DateTime ClassStartTime { get; set; }
        public string ClassTypeName { get; set; }
        public string InstructorName { get; set; }

        public IList<string> InstructorNames { get; set; }

        public IList<SelectListItem> InstructorNameSelectList
        {
            get
            {
                return InstructorNames.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            }
            set { }
        }
    }
}