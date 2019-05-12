using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;

namespace ProperArch01.Persistence.Queries
{
    public class HolidayReader : IHolidayReader
    {
        private readonly ProperArch01DbContext _context;

        public HolidayReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public HolidayDto GetHoliday(string id)
        {
            if (id == null)
            {
                return null;
            }

            var holiday = _context.Holiday.FirstOrDefault(x => x.HolidaysId == id);

            if (holiday == null)
            {
                return null;
            }

            var dto = new HolidayDto() {
                Id = holiday.HolidaysId,
                Name = holiday.Name,
                HolidayDate = holiday.HolidayDate
            };

            return dto;
        }

        public List<HolidayDto> GetAllHolidays()
        {
            var holidays = _context.Holiday.ToList();

            var dtos = holidays.Select(x => new HolidayDto()
            {
                Id = x.HolidaysId,
                Name = x.Name,
                HolidayDate = x.HolidayDate
            }).ToList();

            return dtos;
        }
    }
}