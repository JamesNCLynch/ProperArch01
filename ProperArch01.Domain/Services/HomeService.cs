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
    public class HomeService : IHomeService
    {
        private readonly IClassTypeReader _classTypeReader;
        public HomeService(IClassTypeReader classTypeReader)
        {
            _classTypeReader = classTypeReader;
        }

        public async Task<List<ClassTypeDto>> GetClassTypeDtos()
        {
            var dtos = await _classTypeReader.GetAllActiveClassTypes();
            return dtos.ToList();
        }
    }
}