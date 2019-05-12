using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Commands
{
    public interface IClassAttendanceWriter
    {
        bool AddClassAttendance(ClassAttendanceDto dto);
        bool DeleteClassAttendance(string id);
        bool UpdateClassAttendance(ClassAttendanceDto dto);
    }
}
