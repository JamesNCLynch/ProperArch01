using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Models.ClassType;
using AutoMapper;

namespace ProperArch01.Persistence.Commands
{
    public class ClassTypeWriter : BaseWriter, IClassTypeWriter
    {
        public ClassTypeWriter(ProperArch01DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public bool AddClassType(AddClassTypeModel model)
        {
            var classType = _mapper.Map<ProperArch01.Persistence.EntityModels.ClassType>(model);
            classType.IsActive = true;
            classType.ClassTypeId = Guid.NewGuid().ToString();
            _db.ClassTypes.Add(classType);
            _db.SaveChanges();
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