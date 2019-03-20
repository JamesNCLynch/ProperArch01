using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ScheduledClass;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Queries;
using ProperArch01.Persistence.EntityModels;

namespace ProperArch01.Persistence.Queries
{
    public class ScheduledClassReader : IScheduledClassReader
    {
        private readonly ProperArch01DbContext _context;
        public ScheduledClassReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public List<ScheduledClassDto> GetAllScheduledClasses()
        {
            var scheduledClassDtos = _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor")
                .Select(x => new ScheduledClassDto() {
                Id = x.Id,
                ClassStartTime = x.ClassStartTime,
                ClassTypeName = x.ClassType.Name,
                InstructorName = x.Instructor.UserName,
                IsCancelled = x.IsCancelled
            }).ToList();

            return scheduledClassDtos;
        }

        public ScheduledClassDto GetScheduledClass(string id)
        {
            var scheduledClass = _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor")
                .FirstOrDefault(x => x.Id == id);

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