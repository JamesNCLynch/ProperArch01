using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<ClassTypeDto> GetAllActiveClassTypes()
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

        public async Task<IList<ClassTypeDto>> GetAllActiveClassTypesAsync()
        {
            var classTypes = await _context.ClassTypes.Where(x => x.IsActive == true).ToListAsync();

            var retValue = classTypes.Select(x => new ClassTypeDto()
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

        public async Task<IList<ClassTypeDto>> GetAllClassTypes()
        {
            var classTypes = await _context.ClassTypes.ToListAsync();

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

        public async Task<ClassTypeDto> GetClassType(string id)
        {
            var classType = await _context.ClassTypes.FirstOrDefaultAsync(x => x.Id == id);

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