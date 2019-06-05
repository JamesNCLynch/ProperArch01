using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.Home
{
    public class FooterOpeningHoursViewModel
    {
        public FooterOpeningHoursViewModel()
        {
            OpeningHours = new List<OpeningHourViewModel>();
        }
        public List<OpeningHourViewModel> OpeningHours { get; set; }
        public string BankHolidayNote { get; set; }

        
    }
    public class OpeningHourViewModel
    {
        public string Day { get; set; }
        public string TimeRange { get; set; }
    }
}