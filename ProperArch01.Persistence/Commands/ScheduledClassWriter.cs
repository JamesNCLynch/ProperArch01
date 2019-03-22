using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Commands;
using ProperArch01.Persistence.EntityModels;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProperArch01.Persistence.Commands
{
    public class ScheduledClassWriter : IScheduledClassWriter
    {
        private readonly ProperArch01DbContext _context;
        public ScheduledClassWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddScheduledClass(ScheduledClassDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var instructor = await _context.Users.FirstOrDefaultAsync(x => x.UserName == dto.InstructorName);
            var classType = await _context.ClassTypes.FirstOrDefaultAsync(x => x.Name == dto.ClassTypeName);

            if (instructor == null || classType == null)
            {
                return false;
            }

            var scheduledClass = new ScheduledClass()
            {
                Id = dto.Id,
                Instructor = instructor,
                InstructorId = instructor.Id,
                ClassStartTime = dto.ClassStartTime,
                ClassType = classType,
                ClassTypeId = classType.Id,
                IsCancelled = dto.IsCancelled
            };

            _context.ScheduledClasses.Add(scheduledClass);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteScheduledClass(string id)
        {
            if (id == null)
            {
                return false;
            }

            var scheduledClass = await _context.ScheduledClasses.FirstOrDefaultAsync(x => x.Id == id);

            if (scheduledClass == null)
            {
                return false;
            }

            // TODO: once class attendances is set up, need to return here and delete them

            _context.ScheduledClasses.Remove(scheduledClass);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateScheduledClass(ScheduledClassDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var scheduledClass = await _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor")
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (scheduledClass.ClassType.Name != dto.ClassTypeName)
            {
                var classType = await _context.ClassTypes.FirstOrDefaultAsync(x => x.Name == dto.ClassTypeName);
                if (classType == null)
                {
                    return false;
                }

                scheduledClass.ClassType = classType;
                scheduledClass.ClassTypeId = classType.Id;
            }

            if (scheduledClass.Instructor.UserName != dto.InstructorName)
            {
                var instructor = await _context.Users.FirstOrDefaultAsync(x => x.UserName == dto.InstructorName);
                if (instructor == null)
                {
                    return false;
                }

                scheduledClass.Instructor = instructor;
                scheduledClass.InstructorId = instructor.Id;
            }

            scheduledClass.IsCancelled = dto.IsCancelled;
            scheduledClass.ClassStartTime = dto.ClassStartTime;

            _context.Entry(scheduledClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}