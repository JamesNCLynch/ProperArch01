using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Commands
{
    public interface IScheduledClassWriter
    {
        Task<bool> AddScheduledClass(ScheduledClassDto dto);
        Task<bool> DeleteScheduledClass(string id);
        Task<bool> UpdateScheduledClass(ScheduledClassDto dto);
    }
}
