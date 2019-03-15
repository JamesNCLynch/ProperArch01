using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.Domain.Services
{
    public class ClassTimetableService : IClassTimetableService
    {
        private readonly IClassTimetableReader _classTimetableReader;
        private readonly IClassTimetableWriter _classTimetableWriter;
        private readonly IColourServices _colourServices;

        public ClassTimetableService(IClassTimetableReader classTimetableReader, IClassTimetableWriter classTimetableWriter, IColourServices colourServices)
        {
            _classTimetableReader = classTimetableReader;
            _classTimetableWriter = classTimetableWriter;
            _colourServices = colourServices;
        }

        public bool AddClassTimetable(AddClassTimetableModel model)
        {
            var isSuccess = _classTimetableWriter.AddClassTimetable(model);
            return isSuccess;
        }

        public IEnumerable<ClassTimetableRowViewModel> BuildTimetableViewModel()
        {
            var timetables = _classTimetableReader.GetAllClassTimetables();

            var earliestSlotStartHour = Int32.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]);
            var latestSlotEndHour = Int32.Parse(ConfigurationManager.AppSettings["GymClosingHour"]);

            var timetableViewModel = new List<ClassTimetableRowViewModel>();

            for (int i = earliestSlotStartHour; i <= latestSlotEndHour; i++)
            {
                var row = new ClassTimetableRowViewModel() {
                    StartHour = i
                };

                // get the timetable slots across this week for this time only
                var weekOfClassesAtThisHour = timetables.Where(x => x.StartHour == i).Select(r => new ClassTimetableSlotViewModel() {
                    Day = r.Weekday,
                    StartTime = new DateTime(2000, 1, 1, i, r.StartMinutes, 0),
                    EndTime = new DateTime(2000, 1, 1, r.EndHour, r.EndMinutes, 0),
                    ClassName = r.ClassTypeName,
                    Id = r.Id,
                    Colour = _colourServices.GetRGBAModelFromColourEnum(r.Colour)
                }).ToList();

                foreach (var daySlot in weekOfClassesAtThisHour)
                {
                    switch (daySlot.Day)
                    {
                        case (DayOfWeek.Monday):
                            row.MondayTimeSlot = daySlot;
                            break;
                        case (DayOfWeek.Tuesday):
                            row.TuesdayTimeSlot = daySlot;
                            break;
                        case (DayOfWeek.Wednesday):
                            row.WednesdayTimeSlot = daySlot;
                            break;
                        case (DayOfWeek.Thursday):
                            row.ThursdayTimeSlot = daySlot;
                            break;
                        case (DayOfWeek.Friday):
                            row.FridayTimeSlot = daySlot;
                            break;
                        case (DayOfWeek.Saturday):
                            row.SaturdayTimeSlot = daySlot;
                            break;
                        case (DayOfWeek.Sunday):
                            row.SundayTimeSlot = daySlot;
                            break;
                        default:
                            break;
                    }
                }

                timetableViewModel.Add(row);
            }

            return timetableViewModel.AsEnumerable();
        }

        public bool DeleteClassTimetable(ClassTimetableDto dto)
        {
            var isSuccess = _classTimetableWriter.DeleteClassTimetable(dto);
            return isSuccess;
        }

        public bool EditClassTimetable(EditClassTimetableModel model)
        {
            var isSuccess = _classTimetableWriter.UpdateClassTimetable(model);
            return isSuccess;
        }

        public ClassTimetableDto GetClassTimetable(string id)
        {
            var dto = _classTimetableReader.GetClassTimetable(id);
            return dto;
        }

        public IEnumerable<ClassTimetableDto> GetClassTimetables()
        {
            var timetables = _classTimetableReader.GetAllClassTimetables();
            return timetables;
        }
    }
}