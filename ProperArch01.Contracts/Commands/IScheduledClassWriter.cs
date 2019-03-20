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
        bool AddScheduledClass(ScheduledClassDto dto);
        bool DeleteScheduledClass(string id);
        bool UpdateScheduledClass(ScheduledClassDto dto);
    }
}
