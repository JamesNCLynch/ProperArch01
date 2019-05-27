using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Commands;
using ProperArch01.Persistence.EntityModels;
using System.Data.Entity;
using NLog;

namespace ProperArch01.Persistence.Commands
{
    public class ClassTimetableWriter : IClassTimetableWriter
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public ClassTimetableWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddClassTimetable(ClassTimetableDto dto)
        {
            try
            {
                var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == dto.ClassTypeName);

                if (classType == null)
                {
                    _logger.Warn($"ClassType name {dto.ClassTypeName} does not match anything found in database");
                    return false;
                }

                var classTimetable = new ClassTimetable()
                {
                    Id = Guid.NewGuid().ToString(),
                    ClassType = classType,
                    ClassTypeId = classType.Id,
                    Weekday = dto.Weekday,
                    StartTime = new DateTime(2000, 1, 1, dto.StartHour, dto.StartMinutes, 0),
                    EndTime = new DateTime(2000, 1, 1, dto.EndHour, dto.EndMinutes, 0)
                };

                _context.ClassTimetable.Add(classTimetable);
                _context.SaveChanges();

                _logger.Info($"ClassTimetable {classTimetable.ClassType.Name} starting at {classTimetable.StartTime.ToShortTimeString()} on {classTimetable.Weekday} has been successfully created");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool DeleteClassTimetable(ClassTimetableDto dto)
        {
            try
            {
                var classTimetable = _context.ClassTimetable.FirstOrDefault(x => x.Id == dto.Id);

                if (classTimetable == null)
                {
                    _logger.Warn($"classTimetable ID {dto.Id} does not match anything found in database");
                    return false;
                }

                _context.ClassTimetable.Remove(classTimetable);
                _context.SaveChanges();

                _logger.Info($"ClassTimetable slot {dto.ClassTypeName} at {dto.StartHour}:{dto.StartMinutes} has been deleted");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool UpdateClassTimetable(ClassTimetableDto dto)
        {
            try
            {
                var classTimetable = _context.ClassTimetable.FirstOrDefault(x => x.Id == dto.Id);

                var classType = _context.ClassTypes.FirstOrDefault(x => x.Name == dto.ClassTypeName);

                if (classType == null || classTimetable == null)
                {
                    _logger.Warn($"ClassTimetable ID {dto.Id} or ClassType name {dto.ClassTypeName} not found in database");
                    return false;
                }

                classTimetable.ClassType = classType;
                classTimetable.ClassTypeId = classType.Id;
                classTimetable.StartTime = new DateTime(2000, 1, 1, dto.StartHour, dto.StartMinutes, 0);
                classTimetable.EndTime = new DateTime(2000, 1, 1, dto.EndHour, dto.EndMinutes, 0);
                classTimetable.Weekday = dto.Weekday;

                _context.Entry(classTimetable).State = EntityState.Modified;
                _context.SaveChanges();

                _logger.Info($"ClassTimetable ID {dto.Id} updated");

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