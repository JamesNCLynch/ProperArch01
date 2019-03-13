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
        IEnumerable<ClassTimetableDto> GetClassTimetables();
        bool AddClassTimetable(AddClassTimetableModel model);
        bool EditClassTimetable(EditClassTimetableModel model);
        bool DeleteClassTimetable(ClassTimetableDto dto);
        ClassTimetableDto GetClassTimetable(string id);
        IEnumerable<ClassTimetableRowViewModel> BuildTimetableViewModel();
    }
}
