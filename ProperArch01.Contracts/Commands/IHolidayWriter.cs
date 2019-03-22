using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Holiday;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Commands
{
    public interface IHolidayWriter
    {
        Task<bool> AddHoliday(HolidayDto viewModel);
        Task<bool> DeleteHoliday(string id);
        Task<bool> UpdateHoliday(HolidayDto dto);
    }
}
