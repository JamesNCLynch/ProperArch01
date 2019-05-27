using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;
using ProperArch01.Persistence.EntityModels;
using NLog;

namespace ProperArch01.Persistence.Commands
{
    public class ClassTypeWriter : IClassTypeWriter
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ClassTypeWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddClassType(ClassTypeDto dto)
        {
            try
            {
                var classType = new ClassType
                {
                    Id = dto.Id,
                    IsActive = true,
                    ClassColour = dto.ClassColour,
                    Name = dto.Name,
                    Description = dto.Description,
                    Difficulty = dto.Difficulty,
                    ImageFileName = dto.ImageFileName
                };

                _context.ClassTypes.Add(classType);
                _context.SaveChanges();

                _logger.Info($"ClassType {classType.Name} successfully created");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool DeleteClassType(string id)
        {
            try
            {
                if (id == null)
                {
                    _logger.Warn("Paramater passed is null");
                    return false;
                }
                var classType = _context.ClassTypes.FirstOrDefault(x => x.Id == id);

                if (classType != null)
                {
                    // remove any classtimetables and scheduledclasses that have this classtype
                    var linkedClassTimetables = _context.ClassTimetable.Where(x => x.ClassTypeId == id);

                    if (linkedClassTimetables != null)
                    {
                        _context.ClassTimetable.RemoveRange(linkedClassTimetables);
                        _logger.Info($"{linkedClassTimetables.Count()} ClassTimetables derived from classtype ID {classType.Id} have been deleted");
                    }

                    var linkedScheduledClasses = _context.ScheduledClasses.Where(x => x.ClassTypeId == id);

                    if (linkedScheduledClasses != null)
                    {
                        var linkedClassAttendances = _context.ClassAttendances.Where(x => linkedScheduledClasses.ToList().Contains(x.ScheduledClass));
                        if (linkedClassAttendances != null)
                        {
                            _context.ClassAttendances.RemoveRange(linkedClassAttendances);
                            _logger.Info($"{linkedClassAttendances.Count()} ClassAttendances derived from classtype ID {classType.Id} have been deleted");
                        }
                        
                        _context.ScheduledClasses.RemoveRange(linkedScheduledClasses);
                        _logger.Info($"{linkedScheduledClasses.Count()} ScheduledClasses derived from classtype ID {classType.Id} have been deleted");
                    }

                    // remove classtype and save
                    _context.ClassTypes.Remove(classType);
                    _context.SaveChanges();

                    _logger.Info($"Classtype ID {classType.Id} ({classType.Name}) has been deleted");

                    return true;
                }
                else
                {
                    _logger.Warn($"Classtype ID {id} was not found");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool EditClassType(ClassTypeDto dto)
        {
            try
            {
                if (dto.Id == null)
                {
                    _logger.Warn($"Paramater's ID {dto.Id} passed is null");
                    return false;
                }

                var classType = _context.ClassTypes.FirstOrDefault(x => x.Id == dto.Id);

                if (classType != null)
                {
                    classType.IsActive = dto.IsActive;
                    classType.Name = dto.Name;
                    classType.Difficulty = dto.Difficulty;
                    classType.Description = dto.Description;
                    classType.ClassColour = dto.ClassColour;
                    classType.ImageFileName = dto.ImageFileName;

                    _context.Entry(classType).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    _logger.Info($"ClassTimetable ID {dto.Id} updated");

                    return true;
                }

                _logger.Warn($"Classtype ID {dto.Id} not found in database");
                return false;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }
    }
}