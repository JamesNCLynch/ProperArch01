using ProperArch01.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.Manage
{
    public class AccountIndexViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }        
        public string Email { get; set; }        
        public string UserName { get; set; }
        public List<ScheduledClassDto> ScheduledClasses { get; set; }
        public List<ClassAttendanceDto> ClassesAttended { get; set; }
    }
}