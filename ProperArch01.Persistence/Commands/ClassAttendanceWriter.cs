﻿using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using ProperArch01.Persistence.EntityModels;

namespace ProperArch01.Persistence.Commands
{
    public class ClassAttendanceWriter : IClassAttendanceWriter
    {
        private readonly ProperArch01DbContext _context;
        public ClassAttendanceWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddClassAttendance(ClassAttendanceDto dto)
        {
            var attendee = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.AttendeeId);
            var scheduledClass = await _context.ScheduledClasses.FirstOrDefaultAsync(x => x.Id == dto.ScheduledClassId);

            if (attendee == null || scheduledClass == null)
            {
                return false;
            }

            var classAttendance = new ClassAttendance()
            {
                Id = dto.Id,
                EnrolledDate = dto.EnrolledDate,
                EnrolledBy = dto.EnrolledBy,
                AttendeeId = dto.AttendeeId,
                Attendee = attendee,
                ScheduledClass = scheduledClass,
                ScheduledClassId = dto.ScheduledClassId,
                NoShow = dto.NoShow
            };

            _context.ClassAttendances.Add(classAttendance);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClassAttendance(string id)
        {
            if (id == null)
            {
                return false;
            }

            var classAttendance = await _context.ClassAttendances.FirstOrDefaultAsync(x => x.Id == id);

            if (classAttendance == null)
            {
                return false;
            }

            _context.ClassAttendances.Remove(classAttendance);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateClassAttendance(ClassAttendanceDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var classAttendance = await _context.ClassAttendances.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (classAttendance == null)
            {
                return false;
            }

            if (classAttendance.AttendeeId != dto.AttendeeId)
            {
                var attendee = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.AttendeeId);

                classAttendance.Attendee = attendee;
                classAttendance.AttendeeId = dto.AttendeeId;
            }

            if (classAttendance.ScheduledClassId != dto.ScheduledClassId)
            {
                var scheduledClass = await _context.ScheduledClasses.FirstOrDefaultAsync(x => x.Id == dto.ScheduledClassId);

                classAttendance.ScheduledClass = scheduledClass;
                classAttendance.ScheduledClassId = dto.ScheduledClassId;
            }

            classAttendance.EnrolledBy = dto.EnrolledBy;
            classAttendance.EnrolledDate = dto.EnrolledDate;
            classAttendance.NoShow = dto.NoShow;

            _context.Entry(classAttendance).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}