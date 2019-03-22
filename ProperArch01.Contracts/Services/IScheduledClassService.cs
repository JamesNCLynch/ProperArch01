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
        Task<ScheduledClassIndexViewModel> BuildIndexViewModel();
        Task<ScheduledClassDto> GetScheduledClass(string id);
        Task<bool> AddScheduledClass(CreateScheduledClassViewModel viewModel);
        Task<bool> UpdateScheduledClass(EditScheduledClassViewModel viewModel);
        Task<bool> DeleteScheduledClass(string id);
        Task<List<string>> GetAllInstructorNames();
    }
}
