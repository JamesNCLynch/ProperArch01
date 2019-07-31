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
            var weekdayOpeningHour = new DateTime(2000, 1, 1, int.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]), 0, 0);
            var weekdayClosingHour =  new DateTime(2000, 1, 1, int.Parse(ConfigurationManager.AppSettings["GymClosingHour"]), 0, 0);
            var saturdayOpeningHour = new DateTime(2000, 1, 1, int.Parse(ConfigurationManager.AppSettings["GymOpeningHourSaturday"]), 0, 0);
            var saturdayClosingHour = new DateTime(2000, 1, 1, int.Parse(ConfigurationManager.AppSettings["GymClosingHourSaturday"]), 0, 0);
            var sundayOpeningHour =   new DateTime(2000, 1, 1, int.Parse(ConfigurationManager.AppSettings["GymOpeningHourSunday"]), 0, 0);
            var sundayClosingHour = new DateTime(2000, 1, 1, int.Parse(ConfigurationManager.AppSettings["GymClosingHourSunday"]), 0, 0);

            var viewModel = new FooterOpeningHoursViewModel();

            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Monday",
                    TimeRange = weekdayOpeningHour.ToString("h:mm tt") + " - " + weekdayClosingHour.ToString("hh:mm tt")
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Tuesday",
                    TimeRange = weekdayOpeningHour.ToString("h:mm tt") + " - " + weekdayClosingHour.ToString("hh:mm tt")
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Wednesday",
                    TimeRange = weekdayOpeningHour.ToString("h:mm tt") + " - " + weekdayClosingHour.ToString("hh:mm tt")
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Thursday",
                    TimeRange = weekdayOpeningHour.ToString("h:mm tt") + " - " + weekdayClosingHour.ToString("hh:mm tt")
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Friday",
                    TimeRange = weekdayOpeningHour.ToString("h:mm tt") + " - " + weekdayClosingHour.ToString("hh:mm tt")
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Saturday",
                    TimeRange = saturdayOpeningHour.ToString("h:mm tt") + " - " + saturdayClosingHour.ToString("hh:mm tt")
                }
            );
            viewModel.OpeningHours.Add(
                new OpeningHourViewModel
                {
                    Day = "Sunday",
                    TimeRange = sundayOpeningHour.ToString("h:mm tt") + " - " + sundayClosingHour.ToString("hh:mm tt")
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