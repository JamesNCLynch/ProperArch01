using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Queries;

namespace ProperArch01.Persistence.Queries
{
    public class ClassTimetableReader : IClassTimetableReader
    {
        private readonly ProperArch01DbContext _context;
        public ClassTimetableReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public IList<ClassTimetableDto> GetAllClassTimetables()
        {
            var classTimetables = _context.ClassTimetable
                .Include("ClassType")
                .Select(x => new ClassTimetableDto() {
                Id = x.Id,
                ClassTypeName = x.ClassType.Name,
                StartHour = x.StartTime.Hour,
                StartMinutes = x.StartTime.Minute,
                EndHour = x.EndTime.Hour,
                EndMinutes = x.EndTime.Minute,
                Weekday = x.Weekday,
                Colour = x.ClassType.ClassColour
            }).ToList();

            return classTimetables;
        }

        public ClassTimetableDto GetClassTimetable(string id)
        {
            var classTimetable = _context.ClassTimetable
                .Include("ClassType")
                .FirstOrDefault(x => x.Id == id);

            var dto = new ClassTimetableDto() {
                Id = classTimetable.Id,
                ClassTypeName = classTimetable.ClassType.Name,
                StartHour = classTimetable.StartTime.Hour,
                StartMinutes = classTimetable.StartTime.Minute,
                EndHour = classTimetable.EndTime.Hour,
                EndMinutes = classTimetable.EndTime.Minute,
                Weekday = classTimetable.Weekday,
                Colour = classTimetable.ClassType.ClassColour
            };

            return dto;
        }
    }
}