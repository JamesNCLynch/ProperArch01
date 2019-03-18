using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.Holiday;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;

namespace ProperArch01.Domain.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayReader _holidayReader;
        private readonly IHolidayWriter _holidayWriter;
        
        public HolidayService(IHolidayReader holidayReader, IHolidayWriter holidayWriter)
        {
            _holidayReader = holidayReader;
            _holidayWriter = holidayWriter;
        }

        public bool AddHoliday(CreateHolidayViewModel viewModel)
        {
            var dto = new HolidayDto(viewModel);

            var isSuccess = _holidayWriter.AddHoliday(dto);
            return isSuccess;
        }

        public bool DeleteHoliday(string id)
        {
            var isSuccess = _holidayWriter.DeleteHoliday(id);
            return isSuccess;
        }

        public bool EditHoliday(EditHolidayViewModel viewModel)
        {
            var dto = new HolidayDto(viewModel);
            var isSuccess = _holidayWriter.UpdateHoliday(dto);
            return isSuccess;
        }

        public IList<HolidayDto> GetAllHolidays()
        {
            var holidays = _holidayReader.GetAllHolidays();
            return holidays;
        }

        public HolidayDto GetHoliday(string id)
        {
            var holiday = _holidayReader.GetHoliday(id);
            return holiday;
        }
    }
}