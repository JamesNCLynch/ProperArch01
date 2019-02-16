using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Models.ClassType;

namespace ProperArch01.Persistence.Commands
{
    public class ClassTypeWriter : IClassTypeWriter
    {
        private readonly ProperArch01DbContext _context;
        public ClassTypeWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddClassType(AddClassTypeModel model)
        {
            var classType = new EntityModels.ClassType
            {
                Id = Guid.NewGuid().ToString(),
                IsActive = true

            };

            _context.ClassTypes.Add(classType);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteClassType(string id)
        {
            throw new NotImplementedException();
        }

        public bool EditClassType(EditClassTypeModel model)
        {
            throw new NotImplementedException();
        }
    }
}