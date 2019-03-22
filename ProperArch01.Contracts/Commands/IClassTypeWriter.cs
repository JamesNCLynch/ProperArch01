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
        Task<bool> AddClassType(ClassTypeDto model);
        Task<bool> EditClassType(ClassTypeDto model);
        Task<bool> DeleteClassType(string id);
    }
}
