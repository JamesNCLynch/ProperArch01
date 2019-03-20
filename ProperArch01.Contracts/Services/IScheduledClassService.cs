using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ScheduledClass;

namespace ProperArch01.Contracts.Services
{
    public interface IScheduledClassService
    {
        ScheduledClassIndexViewModel BuildIndexViewModel();
        ScheduledClassDto GetScheduledClass(string id);
        bool AddScheduledClass(CreateScheduledClassViewModel viewModel);
        bool UpdateScheduledClass(EditScheduledClassViewModel viewModel);
        bool DeleteScheduledClass(string id);
        List<string> GetAllInstructorNames();
    }
}
