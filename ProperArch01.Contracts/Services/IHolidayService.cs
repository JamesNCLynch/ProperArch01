using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.Holiday;

namespace ProperArch01.Contracts.Services
{
    public interface IHolidayService
    {
        Task<IList<HolidayDto>> GetAllHolidays();
        Task<HolidayDto> GetHoliday(string id);
        Task<bool> AddHoliday(CreateHolidayViewModel viewModel);
        Task<bool> EditHoliday(EditHolidayViewModel viewModel);
        Task<bool> DeleteHoliday(string id);
    }
}
