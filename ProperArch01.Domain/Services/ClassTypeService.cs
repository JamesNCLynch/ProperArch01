using System.Collections.Generic;
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
                Description = viewModel.Description,
                ImageFileName = viewModel.ImageFileName
            };

            var result = _classTypeWriter.AddClassType(dto);
            return await Task.FromResult(result);
        }

        public async Task<ClassTypeDetailsViewModel> BuildClassTypeViewModel(string id)
        {
            var classType = _classTypeReader.GetClassType(id);

            var dtos = _scheduledClassReader.GetScheduledClassesByClassType(classType.Id);

            var topThree = dtos.OrderBy(x => x.ClassStartTime).Take(3).Select(x => new UpcomingClassesViewModel() {
                ScheduledClassId = x.Id,
                ScheduledClassStartTime = $"{x.ClassStartTime.DayOfWeek} {x.ClassStartTime.ToShortTimeString()}"
            }).ToList();

            var classTypeDtos = _classTypeReader.GetAllActiveClassTypes().ToList();

            var viewModel = new ClassTypeDetailsViewModel()
            {
                Id = classType.Id,
                Name = classType.Name,
                ClassColour = classType.ClassColour,
                Description = classType.Description,
                Difficulty = classType.Difficulty,
                UpcomingScheduledClasses = topThree,
                ImageFileName = classType.ImageFileName,
                ClassTypeDtos = classTypeDtos
            };

            return await Task.FromResult(viewModel);
        }

        public async Task<bool> DeleteClassType(string id)
        {
            var result = _classTypeWriter.DeleteClassType(id);
            return await Task.FromResult(result);
        }

        public async Task<bool> EditClassType(EditClassTypeViewModel viewModel)
        {
            if (viewModel == null)
            {
                return await Task.FromResult(false);
            }

            var dto = new ClassTypeDto
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                ClassColour = viewModel.ClassColour,
                Difficulty = viewModel.Difficulty,
                Description = viewModel.Description,
                IsActive = viewModel.IsActive,
                ImageFileName = viewModel.ImageFileName
            };
            var result = _classTypeWriter.EditClassType(dto);
            return await Task.FromResult(result);
        }

        public async Task<IList<string>> GetAllActiveClassTypeNames()
        {
            var classTypes = _classTypeReader.GetAllActiveClassTypes();
            var names = classTypes.Select(x => x.Name).ToList();
            return await Task.FromResult(names);
        }

        public async Task<IList<ClassTypeDto>> GetAllActiveClassTypes()
        {
            var result = _classTypeReader.GetAllActiveClassTypes();
            return await Task.FromResult(result);
        }

        public async Task<IList<ClassTypeDto>> GetAllClassTypes()
        {
            var result = _classTypeReader.GetAllClassTypes();
            return await Task.FromResult(result);
        }

        public async Task<ClassTypeDto> GetClassType(string id)
        {
            var result = _classTypeReader.GetClassType(id);
            return await Task.FromResult(result);
        }
    }
}