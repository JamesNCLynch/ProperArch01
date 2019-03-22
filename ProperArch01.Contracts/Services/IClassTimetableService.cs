using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Services
{
    public interface IClassTimetableService
    {
        Task<IEnumerable<ClassTimetableDto>> GetClassTimetables();
        Task<bool> AddClassTimetable(AddClassTimetableViewModel model);
        Task<bool> EditClassTimetable(EditClassTimetableViewModel model);
        Task<bool> DeleteClassTimetable(ClassTimetableDto dto);
        Task<ClassTimetableDto> GetClassTimetable(string id);
        Task<IEnumerable<ClassTimetableRowViewModel>> BuildTimetableViewModel();
    }
}
