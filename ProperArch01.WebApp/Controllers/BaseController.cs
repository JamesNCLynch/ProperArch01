using ProperArch01.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProperArch01.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public readonly IBaseService _baseService;
        public readonly IClassAttendanceService _classAttendanceService;
        public readonly IAccountService _accountService;
        public readonly IClassTimetableService _classTimetableService;
        public readonly IClassTypeService _classTypeService;
        public readonly IHolidayService _holidayService;
        public readonly IScheduledClassService _scheduledClassService;
        private IClassAttendanceService classAttendanceService;
        private IAccountService accountService;
        private IClassTimetableService classTimetableService;
        private IClassTypeService classTypeService;
        private IHolidayService holidayService;
        private IScheduledClassService scheduledClassService;

        public BaseController(IBaseService baseService, 
            IScheduledClassService scheduledClassService,
            IHolidayService holidayService,
            IClassTimetableService classTimetableService, 
            IClassTypeService classTypeService,
            IAccountService accountService,
            IClassAttendanceService classAttendanceService)
        {
            _baseService = baseService;
            _classAttendanceService = classAttendanceService;
            _accountService = accountService;
            _classTimetableService = classTimetableService;
            _classTypeService = classTypeService;
            _holidayService = holidayService;
            _scheduledClassService = scheduledClassService;
        }

        public BaseController()
        {

        }

        public BaseController(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public BaseController(IClassAttendanceService classAttendanceService)
        {
            this.classAttendanceService = classAttendanceService;
        }

        public BaseController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public BaseController(IClassTimetableService classTimetableService)
        {
            this.classTimetableService = classTimetableService;
        }

        public BaseController(IClassTypeService classTypeService)
        {
            this.classTypeService = classTypeService;
        }

        public BaseController(IHolidayService holidayService)
        {
            this.holidayService = holidayService;
        }

        public BaseController(IScheduledClassService scheduledClassService)
        {
            this.scheduledClassService = scheduledClassService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var classTypes = _baseService.GetClassTypeDtos();
            ViewBag.ClassTypes = classTypes;

            var openingHours = _baseService.GetFooterOpeningHours();
            ViewBag.OpeningHours = openingHours;

            base.OnActionExecuting(filterContext);
        }
    }
}