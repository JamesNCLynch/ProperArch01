using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassTimetable;

namespace ProperArch01.Contracts.Commands
{
    public interface IClassTimetableWriter
    {
        bool AddClassTimetable(ClassTimetableDto model);
        bool DeleteClassTimetable(ClassTimetableDto dto);
        bool UpdateClassTimetable(ClassTimetableDto model);
    }
}
