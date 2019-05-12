using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Queries
{
    public interface IClassTypeReader
    {
        ClassTypeDto GetClassType(string id);
        IList<ClassTypeDto> GetAllClassTypes();
        //IList<ClassTypeDto> GetAllActiveClassTypesAsync();
        IList<ClassTypeDto> GetAllActiveClassTypes();
    }
}
