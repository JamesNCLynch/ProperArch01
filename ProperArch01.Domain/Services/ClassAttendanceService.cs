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
                return ClassAttendanceResponse.UnspecifiedError;
            }

            var scheduledClass = await _scheduledClassReader.GetScheduledClass(viewModel.ScheduledClassId);
            if (scheduledClass == null)
            {
                return ClassAttendanceResponse.ClassNotFound;
            }
            if (scheduledClass.IsCancelled)
            {
                return ClassAttendanceResponse.ClassCancelled;
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

            bool isSuccess = await _classAttendanceWriter.AddClassAttendance(dto);

            return isSuccess ? ClassAttendanceResponse.Success : ClassAttendanceResponse.UnspecifiedError;
        }

        public async Task<ClassAttendanceIndexViewModel> BuildClassAttendanceIndexViewModel(string id)
        {
            var scheduledClasses = await _scheduledClassReader.GetAllScheduledClasses();
            var classesSignedUp = await _classAttendanceReader.GetAttendancesByUser(id);

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

            return viewModel;
        }

        public async Task<bool> DeleteClassAttendance(string id)
        {
            if (id == null)
            {
                return false;
            }

            var isSuccess = await _classAttendanceWriter.DeleteClassAttendance(id);
            return isSuccess;
        }

        public async Task<bool> EditClassAttendance(EditClassAttendanceViewModel viewModel)
        {
            if (viewModel == null)
            {
                return false;
            }

            var dto = await _classAttendanceReader.GetClassAttendance(viewModel.Id);

            var isSuccess = await _classAttendanceWriter.UpdateClassAttendance(dto);
            return isSuccess;
        }

        // probably won't ever be used
        public async Task<List<ClassAttendanceDto>> GetAllClassAttendances()
        {
            var dtos = await _classAttendanceReader.GetAllClassAttendances();
            return dtos;
        }

        public async Task<ClassAttendanceDto> GetClassAttendance(string id)
        {
            if (id == null)
            {
                return null;
            }

            var dto = await _classAttendanceReader.GetClassAttendance(id);

            if (dto == null)
            {
                return null;
            }

            return dto;
        }

        public async Task<ScheduledClassDto> GetScheduledClass(string scid)
        {
            var dto = await _scheduledClassReader.GetScheduledClass(scid);
            return dto;
        }
    }
}