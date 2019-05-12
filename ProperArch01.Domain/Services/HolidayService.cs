using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.Holiday;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;
using System.Threading.Tasks;

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

        public async Task<bool> AddHoliday(CreateHolidayViewModel viewModel)
        {
            var dto = new HolidayDto() {
                Id = Guid.NewGuid().ToString(),
                Name = viewModel.Name,
                HolidayDate = viewModel.HolidayDate
            };

            var isSuccess = _holidayWriter.AddHoliday(dto);
            return await Task.FromResult(isSuccess);
        }

        public async Task<bool> DeleteHoliday(string id)
        {
            var isSuccess = _holidayWriter.DeleteHoliday(id);
            return await Task.FromResult(isSuccess);
        }

        public async Task<bool> EditHoliday(EditHolidayViewModel viewModel)
        {
            var dto = new HolidayDto()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                HolidayDate = viewModel.HolidayDate
            };

            var isSuccess = _holidayWriter.UpdateHoliday(dto);
            return await Task.FromResult(isSuccess);
        }

        public async Task<IList<HolidayDto>> GetAllHolidays()
        {
            var dtos = _holidayReader.GetAllHolidays();
            var holidays = dtos.OrderByDescending(x => x.HolidayDate).ToList();

            return await Task.FromResult(holidays);
        }

        public async Task<HolidayDto> GetHoliday(string id)
        {
            var holiday = _holidayReader.GetHoliday(id);
            return await Task.FromResult(holiday);
        }
    }
}