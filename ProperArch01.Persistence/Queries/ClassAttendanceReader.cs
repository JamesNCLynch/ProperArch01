using System;
using System.Collections.Generic;
using System.Linq;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;

namespace ProperArch01.Persistence.Queries
{
    public class ClassAttendanceReader : IClassAttendanceReader
    {
        private readonly ProperArch01DbContext _context;
        public ClassAttendanceReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public List<ClassAttendanceDto> GetAllClassAttendances()
        {
            var classAttendances = _context.ClassAttendances
                .Include("Attendee")
                .Include("ScheduledClass")
                .ToList();

            var dtos = classAttendances.Select(x => new ClassAttendanceDto()
            {
                Id = x.Id,
                EnrolledBy = x.EnrolledBy,
                EnrolledDate = x.EnrolledDate,
                AttendeeId = x.AttendeeId,
                AttendeeName = x.Attendee.UserName,
                ScheduledClassId = x.ScheduledClassId,
                ScheduledClassName = x.ScheduledClass.ClassType.Name,
                ClassStartDateTime = x.ScheduledClass.ClassStartTime,
                NoShow = x.NoShow
            }).ToList();

            return dtos;
        }

        public List<ClassAttendanceDto> GetAttendancesByUser(string id)
        {
            var classAttendances = _context.ClassAttendances
                .Include("Attendee")
                .Include("ScheduledClass")
                .Where(x => x.AttendeeId == id).ToList();

            var dtos = classAttendances.Select(x => new ClassAttendanceDto()
            {
                Id = x.Id,
                EnrolledBy = x.EnrolledBy,
                EnrolledDate = x.EnrolledDate,
                AttendeeId = x.AttendeeId,
                AttendeeName = x.Attendee.UserName,
                ScheduledClassId = x.ScheduledClassId,
                ScheduledClassName = x.ScheduledClass.ClassType.Name,
                ClassStartDateTime = x.ScheduledClass.ClassStartTime,
                NoShow = x.NoShow
            }).ToList();

            return dtos;
        }

        public ClassAttendanceDto GetClassAttendance(string id)
        {
            var classAttendance = _context.ClassAttendances
                .Include("Attendee")
                .Include("ScheduledClass")
                .FirstOrDefault(x => x.Id == id);

            var dto = new ClassAttendanceDto()
            {
                Id = classAttendance.Id,
                EnrolledBy = classAttendance.EnrolledBy,
                EnrolledDate = classAttendance.EnrolledDate,
                AttendeeId = classAttendance.AttendeeId,
                AttendeeName = classAttendance.Attendee.UserName,
                ScheduledClassId = classAttendance.ScheduledClassId,
                ScheduledClassName = classAttendance.ScheduledClass.ClassType.Name,
                ClassStartDateTime = classAttendance.ScheduledClass.ClassStartTime,
                NoShow = classAttendance.NoShow
            };

            return dto;
        }

        public List<ClassAttendanceDto> GetClassAttendanceByScheduledClass(string id)
        {
            var classAttendances = _context.ClassAttendances
                .Include("Attendee")
                .Include("ScheduledClass")
                .Where(x => x.ScheduledClassId == id).ToList();

            var dtos = classAttendances.Select(x => new ClassAttendanceDto()
            {
                Id = x.Id,
                EnrolledBy = x.EnrolledBy,
                EnrolledDate = x.EnrolledDate,
                AttendeeId = x.AttendeeId,
                AttendeeName = x.Attendee.UserName,
                ScheduledClassId = x.ScheduledClassId,
                ScheduledClassName = x.ScheduledClass.ClassType.Name,
                ClassStartDateTime = x.ScheduledClass.ClassStartTime,
                NoShow = x.NoShow
            }).ToList();

            return dtos;
        }

        //public List<GymUserDto> GetUsersByScheduledClass(string id)
        //{
        //    var userEntities = _context.ClassAttendances.Where(sc => sc.ScheduledClassId == id).Select(x => x.Attendee).ToList();

        //    var dtos = userEntities.Select(x => new GymUserDto()
        //    {
        //        Id = x.Id,
        //        FirstName = x.FirstName,
        //        LastName = x.LastName,
        //        UserName = x.UserName,
        //        Email = x.Email
        //    }).ToList();

        //    return dtos;
        //}
    }
}