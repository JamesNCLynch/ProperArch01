using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.ClassType;

namespace ProperArch01.Contracts.Services
{
    public interface IClassTypeService
    {
        ClassType GetClassType(string id);
        IList<ClassType> GetAllClassTypes();
        IList<ClassType> GetAllActiveClassTypes();
        bool AddClassType(AddClassTypeModel model);
        bool EditClassType(EditClassTypeModel model);
        bool DeleteClassType(string id);
    }
}
