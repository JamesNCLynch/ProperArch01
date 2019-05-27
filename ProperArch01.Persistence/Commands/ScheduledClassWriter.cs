using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Commands;
using ProperArch01.Persistence.EntityModels;
using System.Data.Entity;
using System.Linq;
using NLog;
using System;

namespace ProperArch01.Persistence.Commands
{
    public class ScheduledClassWriter : IScheduledClassWriter
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public ScheduledClassWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddScheduledClass(ScheduledClassDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var instructor = _context.Users.FirstOrDefault(x => x.UserName == dto.InstructorName);
                var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == dto.ClassTypeName);

                if (instructor == null || classType == null)
                {
                    _logger.Warn($"Instructor {dto.InstructorName} or ClassType {dto.ClassTypeName} not found in database");
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

                _logger.Info($"ScheduledClass ID {dto.Id}, for ClassType {classType.Name} has been scheduled for {scheduledClass.ClassStartTime}");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }

            
        }

        public bool DeleteScheduledClass(string id)
        {
            try
            {
                if (id == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var scheduledClass = _context.ScheduledClasses.FirstOrDefault(x => x.Id == id);

                if (scheduledClass == null)
                {
                    _logger.Warn($"ScheduledClass ID {id} not found in database");
                    return false;
                }

                var linkedClassAttendances = _context.ClassAttendances.Where(x => x.ScheduledClassId == scheduledClass.Id);

                if (linkedClassAttendances != null)
                {
                    _context.ClassAttendances.RemoveRange(linkedClassAttendances);
                    _logger.Info($"{linkedClassAttendances.Count()} ClassAttendances derived from ScheduledClass ID {id} have been deleted");
                }

                _context.ScheduledClasses.Remove(scheduledClass);
                _context.SaveChanges();

                _logger.Info($"ScheduledClass ID {id} has been deleted");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool UpdateScheduledClass(ScheduledClassDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.Warn("Parameter passed is null");
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
                        _logger.Warn($"ClassType {dto.ClassTypeName} not found in database");
                        return false;
                    }

                    scheduledClass.ClassType = classType;
                    scheduledClass.ClassTypeId = classType.Id;
                    _logger.Info($"ScheduledClass ID {scheduledClass.Id} ClassType has been changed to {dto.ClassTypeName}");
                }

                if (scheduledClass.Instructor.UserName != dto.InstructorName)
                {
                    var instructor = _context.Users.FirstOrDefault(x => x.UserName == dto.InstructorName);
                    if (instructor == null)
                    {
                        _logger.Warn($"Instructor {dto.InstructorName} not found in database");
                        return false;
                    }

                    scheduledClass.Instructor = instructor;
                    scheduledClass.InstructorId = instructor.Id;
                    _logger.Info($"ScheduledClass ID {dto.Id} has been changed to {dto.InstructorName}");
                }

                scheduledClass.IsCancelled = dto.IsCancelled;
                scheduledClass.ClassStartTime = dto.ClassStartTime;

                _context.Entry(scheduledClass).State = EntityState.Modified;
                _context.SaveChanges();

                _logger.Info($"ScheduledClass ID {dto.Id} has been updated");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }
    }
}