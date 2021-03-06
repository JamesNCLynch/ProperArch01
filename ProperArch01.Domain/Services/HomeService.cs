﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProperArch01.Contracts.Models.Home;
using ProperArch01.Contracts.Queries;
using ProperArch01.Contracts.Services;

namespace ProperArch01.Domain.Services
{
    public class HomeService : IHomeService
    {
        private readonly IClassTypeReader _classTypeReader;
        private readonly IScheduledClassReader _scheduledClassReader;

        public HomeService(IClassTypeReader classTypeReader, IScheduledClassReader scheduledClassReader)
        {
            _classTypeReader = classTypeReader;
            _scheduledClassReader = scheduledClassReader;
        }

        public async Task<HomeIndexViewModel> BuildIndexViewModel()
        {
            var allUpcomingClasses = _scheduledClassReader.GetAllScheduledClasses();
            var nextThreeUpcomingClasses = allUpcomingClasses.Where(t => t.ClassStartTime > DateTime.UtcNow)
                .OrderBy(x => x.ClassStartTime).Take(2).ToList();

            var allClassTypes = _classTypeReader.GetAllClassTypes();
            var threeUpcomingClassTypes = allClassTypes.Where(x => nextThreeUpcomingClasses.Select(u => u.ClassTypeName).Contains(x.Name)).ToList();

            var upcomingClassViewModels = nextThreeUpcomingClasses.Select(x => new UpcomingClassViewModel() {
                ClassTypeName = x.ClassTypeName,
                ClassTypeId = allClassTypes.FirstOrDefault(u => u.Name == x.ClassTypeName).Id,
                NextScheduledClassStartTime = $"{x.ClassStartTime.DayOfWeek} {x.ClassStartTime.ToShortTimeString()}"
            }).ToList();

            var viewModel = new HomeIndexViewModel() {
                UpcomingClasses = upcomingClassViewModels
            };

            return await Task.FromResult(viewModel);
        }

        public async Task<List<string>> GetListOfGalleryFiles()
        {
            var galleryFilePath = ConfigurationManager.AppSettings["GalleryAssetPath"];

            var filteredByFilename = Directory
                        .EnumerateFiles(HttpContext.Current.Server.MapPath(galleryFilePath))
                        .Select(f => Path.GetFileName(f))
                        .Where(f => f.ToLower().EndsWith(".png") || f.ToLower().EndsWith(".jpg"))
                        .ToList();

            return await Task.FromResult(filteredByFilename);
        }

        //Task<HomeIndexViewModel> IHomeService.BuildIndexViewModel()
        //{
        //    throw new NotImplementedException();
        //}
    }
}