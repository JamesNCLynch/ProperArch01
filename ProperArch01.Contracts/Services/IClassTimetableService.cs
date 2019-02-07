using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.ClassTimetable;

namespace ProperArch01.Contracts.Services
{
    public interface IClassTimetableService
    {
        IEnumerable<ClassTimetable> GetClassTimetables();
        bool AddClassTimetable(AddClassTimetableModel model);
        bool EditClassTimetable(EditClassTimetableModel model);
        bool DeleteClassTimetable(string id);
    }
}
