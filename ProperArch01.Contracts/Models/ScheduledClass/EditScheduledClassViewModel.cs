using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using System.Web.Mvc;

namespace ProperArch01.Contracts.Models.ScheduledClass
{
    public class EditScheduledClassViewModel
    {
        public EditScheduledClassViewModel()
        {

        }

        public EditScheduledClassViewModel(ScheduledClassDto dto)
        {
            Id = dto.Id;
            ClassStartTime = dto.ClassStartTime;
            ClassTypeName = dto.ClassTypeName;
            InstructorName = dto.InstructorName;
            IsCancelled = dto.IsCancelled;
        }

        public EditScheduledClassViewModel(ScheduledClassDto dto, List<string> instructorNames)
        {
            Id = dto.Id;
            ClassStartTime = dto.ClassStartTime;
            ClassTypeName = dto.ClassTypeName;
            InstructorName = dto.InstructorName;
            IsCancelled = dto.IsCancelled;
            InstructorNames = instructorNames;
        }

        public string Id { get; set; }
        public DateTime ClassStartTime { get; set; }
        public string ClassTypeName { get; set; }
        public string InstructorName { get; set; }
        public bool IsCancelled { get; set; }

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