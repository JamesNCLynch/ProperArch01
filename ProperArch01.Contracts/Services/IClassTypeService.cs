﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Services
{
    public interface IClassTypeService
    {
        ClassTypeDto GetClassType(string id);
        IList<ClassTypeDto> GetAllClassTypes();
        IList<ClassTypeDto> GetAllActiveClassTypes();
        IList<string> GetAllActiveClassTypeNames();
        bool AddClassType(AddClassTypeViewModel model);
        bool EditClassType(ClassTypeDto model);
        bool DeleteClassType(string id);
    }
}
