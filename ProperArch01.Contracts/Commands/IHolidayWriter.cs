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
        bool AddHoliday(HolidayDto viewModel);
        bool DeleteHoliday(string id);
        bool UpdateHoliday(HolidayDto dto);
    }
}
