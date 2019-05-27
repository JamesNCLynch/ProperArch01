using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;
using NLog;

namespace ProperArch01.Persistence.Queries
{
    public class ClassTypeReader : IClassTypeReader
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ClassTypeReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public IList<ClassTypeDto> GetAllActiveClassTypes()
        {
            var dtos = _context.ClassTypes.Where(x => x.IsActive == true).Select(x => new ClassTypeDto()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Difficulty = x.Difficulty,
                Description = x.Description,
                ClassColour = x.ClassColour,
                ImageFileName = x.ImageFileName
            }).ToList();

            //_logger.Trace($"{dtos.Count()} active ClassTypes found in database");

            return dtos;
        }

        public IList<ClassTypeDto> GetAllClassTypes()
        {
            var classTypes = _context.ClassTypes.ToList();

            var dtos = classTypes.Select(x => new ClassTypeDto() {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Difficulty = x.Difficulty,
                Description = x.Description,
                ClassColour = x.ClassColour,
                ImageFileName = x.ImageFileName
            }).ToList();

            _logger.Info($"{dtos.Count()} ClassTypes found in database");

            return dtos;
        }

        public ClassTypeDto GetClassType(string id)
        {
            var classType = _context.ClassTypes.FirstOrDefault(x => x.Id == id);

            if (classType == null)
            {
                _logger.Warn($"ClassType ID {id} not found in database");
            }

            var dto = new ClassTypeDto()
            {
                Id = classType.Id,
                Name = classType.Name,
                IsActive = classType.IsActive,
                Difficulty = classType.Difficulty,
                Description = classType.Description,
                ClassColour = classType.ClassColour,
                ImageFileName = classType.ImageFileName
            };

            _logger.Info($"ClassType ID {id} found in database");

            return dto;
        }
    }
}