using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;
using NLog;

namespace ProperArch01.Persistence.Queries
{
    public class HolidayReader : IHolidayReader
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public HolidayReader(ProperArch01DbContext context)
        {
            _context = context;
        }

        public HolidayDto GetHoliday(string id)
        {
            if (id == null)
            {
                _logger.Warn("null parameter passed");
                return null;
            }

            var holiday = _context.Holiday.FirstOrDefault(x => x.HolidaysId == id);

            if (holiday == null)
            {
                _logger.Warn($"Holiday ID {id} not found in database");
                return null;
            }

            var dto = new HolidayDto() {
                Id = holiday.HolidaysId,
                Name = holiday.Name,
                HolidayDate = holiday.HolidayDate
            };

            _logger.Info($"Holiday ID {id} found in database");

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

            _logger.Info($"{holidays.Count()} Holidays found in database");

            return dtos;
        }
    }
}