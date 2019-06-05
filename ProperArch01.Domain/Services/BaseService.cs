using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.Home;
using ProperArch01.Contracts.Queries;
using ProperArch01.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace ProperArch01.Domain.Services
{
    public class BaseService : IBaseService
    {
        private readonly IClassTypeReader _classTypeReader;
        private readonly IHolidayReader _holidayReader;
        public BaseService(IClassTypeReader classTypeReader, IHolidayReader holidayReader)
        {
            _classTypeReader = classTypeReader;
            _holidayReader = holidayReader;
        }

        public List<ClassTypeDto> GetClassTypeDtos()
        {
            var dtos = _classTypeReader.GetAllActiveClassTypes().ToList();
            return dtos;
        }

        public FooterOpeningHoursViewModel GetFooterOpeningHours()
        {
            var weekdayOpeningHour = int.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]);
            var weekdayClosingHour = int.Parse(ConfigurationManager.AppSettings["GymClosingHour"]);
            var saturdayOpeningHour = int.Parse(ConfigurationManager.AppSettings["GymOpeningHourSaturday"]);
            var saturdayClosingHour = int.Parse(ConfigurationManager.AppSettings["GymClosingHourSaturday"]);
            var sundayOpeningHour = int.Parse(ConfigurationManager.AppSettings["GymOpeningHourSunday"]);
            var sundayClosingHour = int.Parse(ConfigurationManager.AppSettings["GymClosingHourSunday"]);

            var viewModel = new FooterOpeningHoursViewModel();

            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Monday",
                    TimeRange = weekdayOpeningHour + ":00 - " + weekdayClosingHour + ":00"
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Tuesday",
                    TimeRange = weekdayOpeningHour + ":00 - " + weekdayClosingHour + ":00"
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Wednesday",
                    TimeRange = weekdayOpeningHour + ":00 - " + weekdayClosingHour + ":00"
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Thursday",
                    TimeRange = weekdayOpeningHour + ":00 - " + weekdayClosingHour + ":00"
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Friday",
                    TimeRange = weekdayOpeningHour + ":00 - " + weekdayClosingHour + ":00"
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Saturday",
                    TimeRange = saturdayOpeningHour + ":00 - " + saturdayClosingHour + ":00"
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Sunday",
                    TimeRange = sundayOpeningHour + ":00 - " + sundayClosingHour + ":00"
                }
            );

            var holidays = _holidayReader.GetAllHolidays();
            var nextHoliday = holidays.OrderBy(x => x.HolidayDate).FirstOrDefault(x => x.HolidayDate >= DateTime.UtcNow);

            if (nextHoliday != null)
            {
                var isNextHolidayWithinFortnight = nextHoliday.HolidayDate < DateTime.UtcNow.AddDays(14);
                if (isNextHolidayWithinFortnight)
                {
                    viewModel.BankHolidayNote = $"Next public holiday is on {nextHoliday.HolidayDate.ToShortDateString()}";
                }
            }

            return viewModel;
        }
    }
}