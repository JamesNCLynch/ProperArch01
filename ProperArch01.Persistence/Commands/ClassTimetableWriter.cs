using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Commands;
using ProperArch01.Persistence.EntityModels;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProperArch01.Persistence.Commands
{
    public class ClassTimetableWriter : IClassTimetableWriter
    {
        private readonly ProperArch01DbContext _context;
        public ClassTimetableWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddClassTimetable(ClassTimetableDto model)
        {
            var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == model.ClassTypeName);

            if (classType == null)
            {
                return false;
            }

            var classTimetable = new ClassTimetable()
            {
                Id = Guid.NewGuid().ToString(),
                ClassType = classType,
                ClassTypeId = classType.Id,
                Weekday = model.Weekday,
                StartTime = new DateTime(2000, 1, 1, model.StartHour, model.StartMinutes, 0),
                EndTime = new DateTime(2000, 1, 1, model.EndHour, model.EndMinutes, 0)
            };

            _context.ClassTimetable.Add(classTimetable);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClassTimetable(ClassTimetableDto dto)
        {
            var classTimetable = _context.ClassTimetable.FirstOrDefault(x => x.Id == dto.Id);

            if (classTimetable == null)
            {
                return false;
            }

            _context.ClassTimetable.Remove(classTimetable);
            await _context.SaveChangesAsync();

            return true;

            // nothing linked to entity so safe to delete
            // however, scheduled classes should be linked, so there may have been a flaw in the original model design
        }

        public async Task<bool> UpdateClassTimetable(ClassTimetableDto model)
        {
            var classTimetable = _context.ClassTimetable.FirstOrDefault(x => x.Id == model.Id);

            var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == model.ClassTypeName);

            if (classType == null)
            {
                return false;
            }

            classTimetable.ClassType = classType;
            classTimetable.ClassTypeId = classType.Id;
            classTimetable.StartTime = new DateTime(2000, 1, 1, model.StartHour, model.StartMinutes, 0);
            classTimetable.EndTime = new DateTime(2000, 1, 1, model.EndHour, model.EndMinutes, 0);
            classTimetable.Weekday = model.Weekday;

            _context.Entry(classTimetable).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}