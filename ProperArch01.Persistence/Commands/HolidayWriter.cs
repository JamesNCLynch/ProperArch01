using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Dto;
using ProperArch01.Persistence.EntityModels;

namespace ProperArch01.Persistence.Commands
{
    public class HolidayWriter : IHolidayWriter
    {
        private readonly ProperArch01DbContext _context;

        public HolidayWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddHoliday(HolidayDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var holiday = new Holidays()
            {
                HolidaysId = Guid.NewGuid().ToString(),
                Name = dto.Name,
                HolidayDate = dto.HolidayDate
            };

            _context.Holiday.Add(holiday);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteHoliday(string id)
        {
            if (id == null)
            {
                return false;
            }

            var holiday = _context.Holiday.FirstOrDefault(x => x.HolidaysId == id);

            if (holiday == null)
            {
                return false;
            }

            _context.Holiday.Remove(holiday);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateHoliday(HolidayDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var holiday = _context.Holiday.FirstOrDefault(x => x.HolidaysId == dto.Id);

            if (holiday == null)
            {
                return false;
            }

            holiday.Name = dto.Name;
            holiday.HolidayDate = dto.HolidayDate;

            _context.Entry(holiday).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return true;
        }
    }
}