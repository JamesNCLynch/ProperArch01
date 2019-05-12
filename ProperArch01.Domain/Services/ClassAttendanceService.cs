using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassAttendance;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;

namespace ProperArch01.Domain.Services
{
    public class ClassAttendanceService : IClassAttendanceService
    {
        private readonly IClassAttendanceReader _classAttendanceReader;
        private readonly IClassAttendanceWriter _classAttendanceWriter;
        private readonly IScheduledClassReader _scheduledClassReader;

        public ClassAttendanceService(IClassAttendanceReader classAttendanceReader, IClassAttendanceWriter classAttendanceWriter, IScheduledClassReader scheduledClassReader)
        {
            _classAttendanceReader = classAttendanceReader;
            _classAttendanceWriter = classAttendanceWriter;
            _scheduledClassReader = scheduledClassReader;
        }

        public async Task<int> AddClassAttendance(CreateClassAttendanceViewModel viewModel)
        {
            if (viewModel == null)
            {
                return await Task.FromResult(ClassAttendanceResponse.UnspecifiedError);
            }

            var scheduledClass = _scheduledClassReader.GetScheduledClass(viewModel.ScheduledClassId);
            if (scheduledClass == null)
            {
                return await Task.FromResult(ClassAttendanceResponse.ClassNotFound);
            }
            if (scheduledClass.IsCancelled)
            {
                return await Task.FromResult(ClassAttendanceResponse.ClassCancelled);
            }

            var dto = new ClassAttendanceDto()
            {
                Id = Guid.NewGuid().ToString(),
                EnrolledDate = DateTime.UtcNow,
                EnrolledBy = viewModel.EnrolledBy,
                AttendeeId = viewModel.AttendeeId,
                ScheduledClassId = viewModel.ScheduledClassId,
                ScheduledClassName = scheduledClass.ClassTypeName,
                NoShow = false
            };

            bool isSuccess = _classAttendanceWriter.AddClassAttendance(dto);

            return await Task.FromResult(isSuccess ? ClassAttendanceResponse.Success : ClassAttendanceResponse.UnspecifiedError);
        }

        public async Task<ClassAttendanceIndexViewModel> BuildClassAttendanceIndexViewModel(string id)
        {
            var scheduledClasses = _scheduledClassReader.GetAllScheduledClasses();
            var classesSignedUp = _classAttendanceReader.GetAttendancesByUser(id);

            if (scheduledClasses == null || classesSignedUp == null)
            {
                return null;
            }

            var signedUpClassIds = classesSignedUp.Select(x => x.ScheduledClassId).ToList();

            var upcomingScheduledClasses = scheduledClasses.Where(x => x.ClassStartTime > DateTime.UtcNow && !signedUpClassIds.Contains(x.Id)).ToList();

            var viewModel = new ClassAttendanceIndexViewModel()
            {
                UpcomingScheduledClasses = upcomingScheduledClasses,
                ClassesCurrentlySignedUp = classesSignedUp.Where(x => x.ClassStartDateTime > DateTime.UtcNow).ToList(),
                PastClassesAttended = classesSignedUp.Where(x => x.ClassStartDateTime < DateTime.UtcNow).ToList()
            };

            return await Task.FromResult(viewModel);
        }

        public async Task<bool> DeleteClassAttendance(string id)
        {
            if (id == null)
            {
                return await Task.FromResult(false);
            }

            var isSuccess = _classAttendanceWriter.DeleteClassAttendance(id);
            return await Task.FromResult(isSuccess);
        }

        public async Task<bool> EditClassAttendance(EditClassAttendanceViewModel viewModel)
        {
            if (viewModel == null)
            {
                return await Task.FromResult(false);
            }

            var dto = _classAttendanceReader.GetClassAttendance(viewModel.Id);

            var isSuccess = _classAttendanceWriter.UpdateClassAttendance(dto);
            return await Task.FromResult(isSuccess);
        }

        // probably won't ever be used
        public async Task<List<ClassAttendanceDto>> GetAllClassAttendances()
        {
            var dtos = _classAttendanceReader.GetAllClassAttendances();
            return await Task.FromResult(dtos);
        }

        public async Task<ClassAttendanceDto> GetClassAttendance(string id)
        {
            if (id == null)
            {
                return null;
            }

            var dto = _classAttendanceReader.GetClassAttendance(id);

            if (dto == null)
            {
                return null;
            }

            return await Task.FromResult(dto);
        }

        public async Task<ScheduledClassDto> GetScheduledClass(string scid)
        {
            var dto = _scheduledClassReader.GetScheduledClass(scid);
            return await Task.FromResult(dto);
        }
    }
}