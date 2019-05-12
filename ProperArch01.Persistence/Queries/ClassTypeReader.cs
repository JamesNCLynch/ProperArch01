using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;

namespace ProperArch01.Persistence.Queries
{
    public class ClassTypeReader : IClassTypeReader
    {
        private readonly ProperArch01DbContext _context;
        public ClassTypeReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public IList<ClassTypeDto> GetAllActiveClassTypes()
        {
            var retValue = _context.ClassTypes.Where(x => x.IsActive == true).Select(x => new ClassTypeDto()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Difficulty = x.Difficulty,
                Description = x.Description,
                ClassColour = x.ClassColour
            });

            return retValue.ToList();
        }

        //public IList<ClassTypeDto> GetAllActiveClassTypes()
        //{
        //    var classTypes = _context.ClassTypes.Where(x => x.IsActive == true).ToList();

        //    var retValue = classTypes.Select(x => new ClassTypeDto()
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        IsActive = x.IsActive,
        //        Difficulty = x.Difficulty,
        //        Description = x.Description,
        //        ClassColour = x.ClassColour
        //    });

        //    return retValue.ToList();
        //}

        public IList<ClassTypeDto> GetAllClassTypes()
        {
            var classTypes = _context.ClassTypes.ToList();

            var retValue = classTypes.Select(x => new ClassTypeDto() {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Difficulty = x.Difficulty,
                Description = x.Description,
                ClassColour = x.ClassColour
            });

            return retValue.ToList();
        }

        public ClassTypeDto GetClassType(string id)
        {
            var classType = _context.ClassTypes.FirstOrDefault(x => x.Id == id);

            var retValue = new ClassTypeDto()
            {
                Id = classType.Id,
                Name = classType.Name,
                IsActive = classType.IsActive,
                Difficulty = classType.Difficulty,
                Description = classType.Description,
                ClassColour = classType.ClassColour
            };

            return retValue;
        }
    }
}