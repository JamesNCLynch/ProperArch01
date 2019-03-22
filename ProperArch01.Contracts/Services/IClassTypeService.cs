using System;
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
        Task<ClassTypeDto> GetClassType(string id);
        Task<IList<ClassTypeDto>> GetAllClassTypes();
        Task<IList<ClassTypeDto>> GetAllActiveClassTypes();
        Task<IList<string>> GetAllActiveClassTypeNames();
        Task<bool> AddClassType(AddClassTypeViewModel model);
        Task<bool> EditClassType(ClassTypeDto model);
        Task<bool> DeleteClassType(string id);
    }
}
