using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;

namespace ProperArch01.Domain.Services
{
    public class ClassTypeService : IClassTypeService
    {
        private readonly IClassTypeReader _classTypeReader;
        private readonly IClassTypeWriter _classTypeWriter;

        public ClassTypeService(IClassTypeReader classTypeReader, IClassTypeWriter classTypeWriter)
        {
            _classTypeReader = classTypeReader;
            _classTypeWriter = classTypeWriter;
        }

        public bool AddClassType(AddClassTypeViewModel model)
        {
            var result = _classTypeWriter.AddClassType(model);
            return result;
        }

        public bool DeleteClassType(string id)
        {
            var result = _classTypeWriter.DeleteClassType(id);
            return result;
        }

        public bool EditClassType(ClassTypeDto model)
        {
            var result = _classTypeWriter.EditClassType(model);
            return result;
        }

        public IList<ClassTypeDto> GetAllActiveClassTypes()
        {
            var result = _classTypeReader.GetAllClassTypes().Where(x => x.IsActive).ToList();
            return result;
        }

        public IList<ClassTypeDto> GetAllClassTypes()
        {
            var result = _classTypeReader.GetAllClassTypes();
            return result;
        }

        public ClassTypeDto GetClassType(string id)
        {
            var result = _classTypeReader.GetClassType(id);
            return result;
        }
    }
}