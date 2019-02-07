using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Services;

namespace ProperArch01.Domain.Services
{
    public class ClassTypeService : IClassTypeService
    {
        public bool AddClassType(AddClassTypeModel model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteClassType(string id)
        {
            throw new NotImplementedException();
        }

        public bool EditClassType(EditClassTypeModel model)
        {
            throw new NotImplementedException();
        }

        public IList<ClassType> GetAllActiveClassTypes()
        {
            throw new NotImplementedException();
        }

        public IList<ClassType> GetAllClassTypes()
        {
            throw new NotImplementedException();
        }

        public ClassType GetClassType(string id)
        {
            throw new NotImplementedException();
        }
    }
}