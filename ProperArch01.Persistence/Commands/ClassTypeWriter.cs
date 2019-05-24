using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;
using System.Data.Entity;

namespace ProperArch01.Persistence.Commands
{
    public class ClassTypeWriter : IClassTypeWriter
    {
        private readonly ProperArch01DbContext _context;
        public ClassTypeWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddClassType(ClassTypeDto dto)
        {
            var classType = new EntityModels.ClassType
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
            return true;
        }

        public bool DeleteClassType(string id)
        {
            if (id == null)
            {
                return false;
            }
            var classType = _context.ClassTypes.FirstOrDefault(x => x.Id == id);

            if (classType != null)
            {
                // remove any classtimetables and scheduledclasses that have this classtype
                var linkedClassTimetables = _context.ClassTimetable.Where(x => x.ClassTypeId == id);
                _context.ClassTimetable.RemoveRange(linkedClassTimetables);
                var linkedScheduledClasses = _context.ScheduledClasses.Where(x => x.ClassTypeId == id);
                _context.ScheduledClasses.RemoveRange(linkedScheduledClasses);

                // remove classtype and save
                _context.ClassTypes.Remove(classType);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool EditClassType(ClassTypeDto dto)
        {
            if (dto.Id == null)
            {
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

                return true;
            }

            return false;
        }
    }
}