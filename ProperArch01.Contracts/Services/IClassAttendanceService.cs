using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassAttendance;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.Contracts.Services
{
    public interface IClassAttendanceService
    {
        Task<List<ClassAttendanceDto>> GetAllClassAttendances();
        Task<ClassAttendanceDto> GetClassAttendance(string id);
        Task<int> AddClassAttendance(CreateClassAttendanceViewModel viewModel);
        Task<bool> EditClassAttendance(EditClassAttendanceViewModel viewModel);
        Task<bool> DeleteClassAttendance(string id);
        Task<ClassAttendanceIndexViewModel> BuildClassAttendanceIndexViewModel(string id);
        Task<ScheduledClassDto> GetScheduledClass(string scid);
    }
}
