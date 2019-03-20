using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ScheduledClass;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Commands;
using ProperArch01.Persistence.EntityModels;

namespace ProperArch01.Persistence.Commands
{
    public class ScheduledClassWriter : IScheduledClassWriter
    {
        private readonly ProperArch01DbContext _context;
        public ScheduledClassWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddScheduledClass(ScheduledClassDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var instructor = _context.Users.FirstOrDefault(x => x.UserName == dto.InstructorName);
            var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == dto.ClassTypeName);

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
            _context.SaveChanges();

            return true;
        }

        public bool DeleteScheduledClass(string id)
        {
            if (id == null)
            {
                return false;
            }

            var scheduledClass = _context.ScheduledClasses.FirstOrDefault(x => x.Id == id);

            if (scheduledClass == null)
            {
                return false;
            }

            // TODO: once class attendances is set up, need to return here and delete them

            _context.ScheduledClasses.Remove(scheduledClass);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateScheduledClass(ScheduledClassDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var scheduledClass = _context.ScheduledClasses
                .Include("ClassType")
                .Include("Instructor")
                .FirstOrDefault(x => x.Id == dto.Id);

            if (scheduledClass.ClassType.Name != dto.ClassTypeName)
            {
                var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == dto.ClassTypeName);
                if (classType == null)
                {
                    return false;
                }

                scheduledClass.ClassType = classType;
                scheduledClass.ClassTypeId = classType.Id;
            }

            if (scheduledClass.Instructor.UserName != dto.InstructorName)
            {
                var instructor = _context.Users.FirstOrDefault(x => x.UserName == dto.InstructorName);
                if (instructor == null)
                {
                    return false;
                }

                scheduledClass.Instructor = instructor;
                scheduledClass.InstructorId = instructor.Id;
            }

            scheduledClass.IsCancelled = dto.IsCancelled;
            scheduledClass.ClassStartTime = dto.ClassStartTime;

            _context.Entry(scheduledClass).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return true;
        }
    }
}