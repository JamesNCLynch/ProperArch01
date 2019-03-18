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
        IList<HolidayDto> GetAllHolidays();
        HolidayDto GetHoliday(string id);
        bool AddHoliday(CreateHolidayViewModel viewModel);
        bool EditHoliday(EditHolidayViewModel viewModel);
        bool DeleteHoliday(string id);
    }
}
