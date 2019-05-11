﻿using System.Collections.Generic;
using System.Linq;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;
using System.Threading.Tasks;
using System;

namespace ProperArch01.Domain.Services
{
    public class ClassTypeService : IClassTypeService
    {
        private readonly IClassTypeReader _classTypeReader;
        private readonly IClassTypeWriter _classTypeWriter;
        private readonly IScheduledClassReader _scheduledClassReader;

        public ClassTypeService(IClassTypeReader classTypeReader, IClassTypeWriter classTypeWriter, IScheduledClassReader scheduledClassReader)
        {
            _classTypeReader = classTypeReader;
            _classTypeWriter = classTypeWriter;
            _scheduledClassReader = scheduledClassReader;
        }

        public async Task<bool> AddClassType(AddClassTypeViewModel viewModel)
        {
            var dto = new ClassTypeDto()
            {
                Id = Guid.NewGuid().ToString(),
                Name = viewModel.Name,
                ClassColour = viewModel.ClassColour,
                Difficulty = viewModel.Difficulty,
                Description = viewModel.Description
            };

            var result = await _classTypeWriter.AddClassType(dto);
            return result;
        }

        public async Task<ClassTypeDetailsViewModel> BuildClassTypeViewModel(string id)
        {
            var classType = await _classTypeReader.GetClassType(id);

            var dtos = await _scheduledClassReader.GetScheduledClassesByClassType(classType.Id);

            var topThree = dtos.OrderBy(x => x.ClassStartTime).Take(3).Select(x => new UpcomingClassesViewModel() {
                ScheduledClassId = x.Id,
                ScheduledClassStartTime = $"{x.ClassStartTime.DayOfWeek} {x.ClassStartTime.ToShortTimeString()}"
            }).ToList();

            var viewModel = new ClassTypeDetailsViewModel()
            {
                Id = classType.Id,
                ClassColour = classType.ClassColour,
                Description = classType.Description,
                Difficulty = classType.Difficulty,
                UpcomingScheduledClasses = topThree
            };

            return viewModel;
        }

        public async Task<bool> DeleteClassType(string id)
        {
            var result = await _classTypeWriter.DeleteClassType(id);
            return result;
        }

        public async Task<bool> EditClassType(EditClassTypeViewModel viewModel)
        {
            if (viewModel == null)
            {
                return false;
            }

            var dto = new ClassTypeDto
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                ClassColour = viewModel.ClassColour,
                Difficulty = viewModel.Difficulty,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive
            };
            var result = await _classTypeWriter.EditClassType(dto);
            return result;
        }

        public async Task<IList<string>> GetAllActiveClassTypeNames()
        {
            var classTypes = await _classTypeReader.GetAllActiveClassTypesAsync();
            var names = classTypes.Select(x => x.Name).ToList();
            return names;
        }

        public async Task<IList<ClassTypeDto>> GetAllActiveClassTypes()
        {
            var result = await _classTypeReader.GetAllActiveClassTypesAsync();
            return result;
        }

        public async Task<IList<ClassTypeDto>> GetAllClassTypes()
        {
            var result = await _classTypeReader.GetAllClassTypes();
            return result;
        }

        public async Task<ClassTypeDto> GetClassType(string id)
        {
            var result = await _classTypeReader.GetClassType(id);
            return result;
        }
    }
}