using System.Collections.Generic;
using System.Linq;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using ProperArch01.Persistence.EntityModels;
using System.Threading.Tasks;
using System.Data.Entity;
using NLog;

namespace ProperArch01.Persistence.Queries
{
    public class ScheduledClassReader : IScheduledClassReader
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ScheduledClassReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public List<ScheduledClassDto> GetAllScheduledClasses()
        {
            var scheduledClasses = _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor");

            var scheduledClassDtos = scheduledClasses
                .Select(x => new ScheduledClassDto() {
                Id = x.Id,
                ClassStartTime = x.ClassStartTime,
                ClassTypeName = x.ClassType.Name,
                InstructorName = x.Instructor.UserName,
                IsCancelled = x.IsCancelled
            }).ToList();

            _logger.Info($"{scheduledClassDtos.Count()} ScheduledClasses found in database");

            return scheduledClassDtos;
        }

        public ScheduledClassDto GetScheduledClass(string id)
        {
            var scheduledClass = _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor")
                .FirstOrDefault(x => x.Id == id);

            if (scheduledClass == null)
            {
                _logger.Warn($"ScheduledClass ID {id} not found in database");
                return null;
            }

            var dto = new ScheduledClassDto()
            {
                Id = scheduledClass.Id,
                ClassStartTime = scheduledClass.ClassStartTime,
                ClassTypeName = scheduledClass.ClassType.Name,
                InstructorName = scheduledClass.Instructor.UserName,
                IsCancelled = scheduledClass.IsCancelled
            };
            
            _logger.Info($"ScheduledClass ID {id} found in database");

            return dto;
        }

        public List<ScheduledClassDto> GetScheduledClassesByClassType(string id)
        {
            var scheduledClasses = _context.ScheduledClasses.Where(x => x.ClassTypeId == id);

            if (scheduledClasses == null)
            {
                _logger.Warn($"ScheduledClass linked to ClassType ID {id} not found in database");
                return null;
            }

            var dtos = scheduledClasses.Select(x => new ScheduledClassDto()
            {
                Id = x.Id,
                ClassStartTime = x.ClassStartTime,
                ClassTypeName = x.ClassType.Name,
                InstructorName = x.Instructor.UserName,
                IsCancelled = x.IsCancelled
            }).ToList();
            
            _logger.Info($"{dtos.Count()} ScheduledClasses linked to ClassType ID {id} found in database");

            return dtos;
        }

        public List<ScheduledClassDto> GetScheduledClassesByUserId(string userId)
        {
            var scheduledClasses = _context.ClassAttendances.Where(ca => ca.AttendeeId == userId).Select(sc => sc.ScheduledClass);

            var dtos = scheduledClasses.Select(x => new ScheduledClassDto()
            {
                Id = x.Id,
                ClassStartTime = x.ClassStartTime,
                ClassTypeName = x.ClassType.Name,
                InstructorName = x.Instructor.UserName,
                IsCancelled = x.IsCancelled
            }).ToList();

            _logger.Info($"{dtos.Count()} ScheduledClasses linked to User ID {userId} found in database");

            return dtos;
        }
    }
}