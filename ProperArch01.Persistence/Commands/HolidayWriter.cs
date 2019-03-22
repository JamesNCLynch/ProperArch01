using System.Threading.Tasks;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Dto;
using ProperArch01.Persistence.EntityModels;
using System.Data.Entity;

namespace ProperArch01.Persistence.Commands
{
    public class HolidayWriter : IHolidayWriter
    {
        private readonly ProperArch01DbContext _context;

        public HolidayWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddHoliday(HolidayDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var holiday = new Holidays()
            {
                HolidaysId = dto.Id,
                Name = dto.Name,
                HolidayDate = dto.HolidayDate
            };

            _context.Holiday.Add(holiday);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteHoliday(string id)
        {
            if (id == null)
            {
                return false;
            }

            var holiday = await _context.Holiday.FirstOrDefaultAsync(x => x.HolidaysId == id);

            if (holiday == null)
            {
                return false;
            }

            _context.Holiday.Remove(holiday);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateHoliday(HolidayDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            var holiday = await _context.Holiday.FirstOrDefaultAsync(x => x.HolidaysId == dto.Id);

            if (holiday == null)
            {
                return false;
            }

            holiday.Name = dto.Name;
            holiday.HolidayDate = dto.HolidayDate;

            _context.Entry(holiday).State = System.Data.Entity.EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}