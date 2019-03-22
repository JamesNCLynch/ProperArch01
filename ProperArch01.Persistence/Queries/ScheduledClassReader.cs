using System.Collections.Generic;
using System.Linq;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using ProperArch01.Persistence.EntityModels;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProperArch01.Persistence.Queries
{
    public class ScheduledClassReader : IScheduledClassReader
    {
        private readonly ProperArch01DbContext _context;
        public ScheduledClassReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduledClassDto>> GetAllScheduledClasses()
        {
            var scheduledClasses = await _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor").ToListAsync();

            var scheduledClassDtos = scheduledClasses
                .Select(x => new ScheduledClassDto() {
                Id = x.Id,
                ClassStartTime = x.ClassStartTime,
                ClassTypeName = x.ClassType.Name,
                InstructorName = x.Instructor.UserName,
                IsCancelled = x.IsCancelled
            }).ToList();

            return scheduledClassDtos;
        }

        public async Task<ScheduledClassDto> GetScheduledClass(string id)
        {
            var scheduledClass = await _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor")
                .FirstOrDefaultAsync(x => x.Id == id);

            var dto = new ScheduledClassDto()
            {
                Id = scheduledClass.Id,
                ClassStartTime = scheduledClass.ClassStartTime,
                ClassTypeName = scheduledClass.ClassType.Name,
                InstructorName = scheduledClass.Instructor.UserName,
                IsCancelled = scheduledClass.IsCancelled
            };

            return dto;
        }
    }
}