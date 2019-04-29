using ProperArch01.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperArch01.Contracts.Services
{
    public interface IBaseService
    {
        Task<List<ClassTypeDto>> GetClassTypeDtosAsync();
        List<ClassTypeDto> GetClassTypeDtos();
    }
}
