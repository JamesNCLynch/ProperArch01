﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassTimetable;

namespace ProperArch01.Contracts.Queries
{
    public interface IClassTimetableReader
    {
        ClassTimetableDto GetClassTimetable(string id);
        IList<ClassTimetableDto> GetAllClassTimetables();
    }
}
