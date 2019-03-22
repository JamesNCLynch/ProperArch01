using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<HolidayDto> GetHoliday(string id)
        {
            if (id == null)
            {
                return null;
            }

            var holiday = await _context.Holiday.FirstOrDefaultAsync(x => x.HolidaysId == id);

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

        public async Task<List<HolidayDto>> GetAllHolidays()
        {
            var holidays = await _context.Holiday.ToListAsync();

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