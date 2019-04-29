using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using ProperArch01.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProperArch01.Domain.Services
{
    public class BaseService : IBaseService
    {
        private readonly IClassTypeReader _classTypeReader;
        public BaseService(IClassTypeReader classTypeReader)
        {
            _classTypeReader = classTypeReader;
        }

        public List<ClassTypeDto> GetClassTypeDtos()
        {
            var dtos = _classTypeReader.GetAllActiveClassTypes();
            return dtos.ToList();
        }

        public async Task<List<ClassTypeDto>> GetClassTypeDtosAsync()
        {
            var dtos = await _classTypeReader.GetAllActiveClassTypesAsync();
            return dtos.ToList();
        }
    }
}