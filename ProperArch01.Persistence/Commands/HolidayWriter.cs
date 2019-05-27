using NLog;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Dto;
using ProperArch01.Persistence.EntityModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace ProperArch01.Persistence.Commands
{
    public class HolidayWriter : IHolidayWriter
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public HolidayWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public bool AddHoliday(HolidayDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var holiday = new Holidays()
                {
                    HolidaysId = dto.Id,
                    Name = dto.Name,
                    HolidayDate = dto.HolidayDate
                };

                _context.Holiday.Add(holiday);
                _context.SaveChanges();

                _logger.Info($"Holiday {dto.Name} on {dto.HolidayDate} has been successfully created");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool DeleteHoliday(string id)
        {
            try
            {
                if (id == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var holiday = _context.Holiday.FirstOrDefault(x => x.HolidaysId == id);

                if (holiday == null)
                {
                    _logger.Warn($"Holiday ID {id} not found in database");
                    return false;
                }

                _context.Holiday.Remove(holiday);
                _context.SaveChanges();

                _logger.Info($"Holiday {holiday.Name} on {holiday.HolidayDate} has been successfully deleted");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool UpdateHoliday(HolidayDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var holiday = _context.Holiday.FirstOrDefault(x => x.HolidaysId == dto.Id);

                if (holiday == null)
                {
                    _logger.Warn($"Holiday ID {dto.Id} not found in database");
                    return false;
                }

                holiday.Name = dto.Name;
                holiday.HolidayDate = dto.HolidayDate;

                _context.Entry(holiday).State = EntityState.Modified;
                _context.SaveChanges();

                _logger.Info($"Holiday {dto.Name} on {dto.HolidayDate} has been updated");

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