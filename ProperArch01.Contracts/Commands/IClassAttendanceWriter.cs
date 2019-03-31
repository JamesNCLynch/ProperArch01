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
        Task<bool> AddClassAttendance(ClassAttendanceDto dto);
        Task<bool> DeleteClassAttendance(string id);
        Task<bool> UpdateClassAttendance(ClassAttendanceDto dto);
    }
}
