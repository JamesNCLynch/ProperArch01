using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Commands
{
    public interface IClassTypeWriter
    {
        bool AddClassType(AddClassTypeViewModel model);
        bool EditClassType(ClassTypeDto model);
        bool DeleteClassType(string id);
    }
}
