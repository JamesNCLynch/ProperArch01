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
        Task<List<ClassAttendanceDto>> GetAttendancesByUser(string id);
        Task<ClassAttendanceDto> GetClassAttendance(string id);
        Task<List<ClassAttendanceDto>> GetAllClassAttendances();
        Task<List<ClassAttendanceDto>> GetClassAttendanceByScheduledClass(string id);
    }
}
