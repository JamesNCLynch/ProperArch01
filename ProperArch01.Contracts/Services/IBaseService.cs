using ProperArch01.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Home;

namespace ProperArch01.Contracts.Services
{
    public interface IBaseService
    {
        List<ClassTypeDto> GetClassTypeDtos();
        FooterOpeningHoursViewModel GetFooterOpeningHours();
    }
}
