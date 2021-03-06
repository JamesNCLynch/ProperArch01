﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Home;

namespace ProperArch01.Contracts.Services
{
    public interface IHomeService
    {
        Task<HomeIndexViewModel> BuildIndexViewModel();
        Task<List<string>> GetListOfGalleryFiles();
    }
}
