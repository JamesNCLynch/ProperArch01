using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ScheduledClass;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;
using ProperArch01.Contracts.Constants;
using System.Threading.Tasks;

namespace ProperArch01.Domain.Services
{
    public class ScheduledClassService : IScheduledClassService
    {
        private readonly IScheduledClassReader _scheduledClassReader;
        private readonly IScheduledClassWriter _scheduledClassWriter;
        private readonly IClassTypeReader _classTypeReader;
        private readonly IClassTimetableReader _classTimetableReader;
        private readonly IHolidayReader _holidayReader;
        private readonly IGymUserReader _gymReader;
        private readonly IClassAttendanceReader _classAttendanceReader;

        public ScheduledClassService(IScheduledClassReader scheduledClassReader, 
            IScheduledClassWriter scheduledClassWriter, 
            IClassTypeReader classTypeReader, 
            IClassTimetableReader classTimetableReader,
            IHolidayReader holidayReader,
            IGymUserReader gymReader,
            IClassAttendanceReader classAttendanceReader)
        {
            _scheduledClassReader = scheduledClassReader;
            _scheduledClassWriter = scheduledClassWriter;
            _classTypeReader = classTypeReader;
            _classTimetableReader = classTimetableReader;
            _holidayReader = holidayReader;
            _gymReader = gymReader;
            _classAttendanceReader = classAttendanceReader;
        }

        public async Task<bool> AddScheduledClass(CreateScheduledClassViewModel viewModel)
        {
            if (viewModel == null)
            {
                return await Task.FromResult(false);
            }

            ScheduledClassDto dto = new ScheduledClassDto()
            {
                Id = Guid.NewGuid().ToString(),
                ClassStartTime = viewModel.ClassStartTime,
                ClassTypeName = viewModel.ClassTypeName,
                InstructorName = viewModel.InstructorName,
                IsCancelled = false
            };

            bool isSuccess = _scheduledClassWriter.AddScheduledClass(dto);

            return await Task.FromResult(isSuccess);
        }

        public async Task<ScheduledClassIndexViewModel> BuildIndexViewModel()
        {
            var allScheduledClasses = _scheduledClassReader.GetAllScheduledClasses();

            var indexViewModel = new ScheduledClassIndexViewModel() {
                ScheduledClassesCompleted = allScheduledClasses.Where(x => x.InstructorName != null),
                CancelledScheduledClasses = allScheduledClasses.Where(x => x.IsCancelled)
            };

            var timetable = _classTimetableReader.GetAllClassTimetables();

            var holidayDtos = _holidayReader.GetAllHolidays();
            var holidayDates = holidayDtos.Where(x => x.HolidayDate > DateTime.UtcNow).Select(x => x.HolidayDate.Date);

            var plannerTimespan = Int32.Parse(ConfigurationManager.AppSettings["ScheduledClassTimeSpanInDays"]);
            var earliestSlotStartHour = Int32.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]);
            var latestSlotEndHour = Int32.Parse(ConfigurationManager.AppSettings["GymClosingHour"]);

            var incompleteScheduledClassSlots = new List<ScheduledClassDto>();

            // loop through day from today until end of timespan
            foreach (var daysFromToday in Enumerable.Range(0, plannerTimespan))
            {
                var iterationDateTime = DateTime.UtcNow.AddDays(daysFromToday);

                // if day falls on holiday
                if (holidayDates.Contains(iterationDateTime.Date))
                {
                    continue;
                }

                foreach (var hourOfToday in Enumerable.Range(earliestSlotStartHour, latestSlotEndHour - earliestSlotStartHour))
                {
                    var isScheduledClassAlreadyCreated = allScheduledClasses.Any(x => x.ClassStartTime.Day == iterationDateTime.Day && x.ClassStartTime.Hour == hourOfToday);
                    if (isScheduledClassAlreadyCreated)
                    {
                        continue;
                    }

                    var timetabledSlot = timetable.FirstOrDefault(x => x.Weekday == iterationDateTime.DayOfWeek && x.StartHour == hourOfToday);
                    if (timetabledSlot == null)
                    {
                        continue;
                    }

                    var newScheduledClass = new ScheduledClassDto()
                    {
                        ClassStartTime = new DateTime(iterationDateTime.Year, iterationDateTime.Month, iterationDateTime.Day, timetabledSlot.StartHour, timetabledSlot.StartMinutes, 0),
                        ClassTypeName = timetabledSlot.ClassTypeName
                    };

                    incompleteScheduledClassSlots.Add(newScheduledClass);
                }
            }

            indexViewModel.ScheduledClassesRequiringCompletion = incompleteScheduledClassSlots;

            return await Task.FromResult(indexViewModel);
        }

        public async Task<DetailedScheduledClassViewModel> BuildScheduledClassDetailsViewModel(string id)
        {
            var dto = _scheduledClassReader.GetScheduledClass(id);
            var attendances = _classAttendanceReader.GetClassAttendanceByScheduledClass(id);

            if (dto == null || attendances == null)
            {
                return null;
            }

            var viewModel = new DetailedScheduledClassViewModel()
            {
                Id = dto.Id,
                ClassStartTime = dto.ClassStartTime,
                ClassTypeName = dto.ClassTypeName,
                InstructorName = dto.InstructorName,
                IsCancelled = dto.IsCancelled,
                Attendances = attendances
            };

            return await Task.FromResult(viewModel);
        }

        public async Task<bool> DeleteScheduledClass(string id)
        {
            var isSuccess = _scheduledClassWriter.DeleteScheduledClass(id);
            return await Task.FromResult(isSuccess);
        }

        public async Task<List<string>> GetAllInstructorNames()
        {
            var instructors = _gymReader.GetAllUsers();
            var instructorNames = instructors.Where(x => x.RoleName == RoleNames.InstructorName).Select(u => u.UserName).ToList();

            return await Task.FromResult(instructorNames);
        }

        public async Task<ScheduledClassDto> GetScheduledClass(string id)
        {
            var dto = _scheduledClassReader.GetScheduledClass(id);
            return await Task.FromResult(dto);
        }

        public async Task<bool> UpdateScheduledClass(EditScheduledClassViewModel viewModel)
        {
            var dto = new ScheduledClassDto()
            {
                Id = viewModel.Id,
                ClassStartTime = viewModel.ClassStartTime,
                ClassTypeName = viewModel.ClassTypeName,
                InstructorName = viewModel.InstructorName,
                IsCancelled = viewModel.IsCancelled
            };

            var isSuccess = _scheduledClassWriter.UpdateScheduledClass(dto);
            return await Task.FromResult(isSuccess);
        }
    }
}