﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Queries
{
    public interface IScheduledClassReader
    {
        List<ScheduledClassDto> GetAllScheduledClasses();
        ScheduledClassDto GetScheduledClass(string id);
    }
}
