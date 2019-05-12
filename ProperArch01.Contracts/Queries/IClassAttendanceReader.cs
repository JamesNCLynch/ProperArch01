using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Queries
{
    public interface IClassAttendanceReader
    {
        List<ClassAttendanceDto> GetAttendancesByUser(string id);
        ClassAttendanceDto GetClassAttendance(string id);
        List<ClassAttendanceDto> GetAllClassAttendances();
        List<ClassAttendanceDto> GetClassAttendanceByScheduledClass(string id);
    }
}
