﻿using System;
using System.Web;
using System.Net;
using System.Web.Mvc;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;
using System.Threading.Tasks;
using System.Configuration;
using NLog;

namespace ProperArch01.WebApp.Controllers
{
    public class ClassTimetableController : BaseController
    {
        new private readonly IClassTimetableService _classTimetableService;
        new private readonly IClassTypeService _classTypeService;
        new private readonly IBaseService _baseService;
        
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ClassTimetableController(IClassTimetableService classTimetableService, IClassTypeService classTypeService, IBaseService baseService) : base(baseService)
        {
            _classTimetableService = classTimetableService;
            _classTypeService = classTypeService;
            _baseService = baseService;
        }

        // GET: ClassTimetable
        public async Task<ActionResult> Index()
        {
            var classTimetableViewModel = await _classTimetableService.BuildTimetableViewModel();
            return View(classTimetableViewModel);
        }

        // GET: ClassTimetable/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                _logger.Trace("No parameters passed for Details");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTimetable = await _classTimetableService.GetClassTimetable(id);

            if (classTimetable == null)
            {
                _logger.Info($"Parameter {id} was passed for Details but did not return a result");
                return HttpNotFound();
            }

            return View(classTimetable);
        }

        // GET: ClassTimetable/Create
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Create(int weekday, int startHour)
        {
            if (weekday < 0 || weekday > 7)
            {
                _logger.Info($"An attempt was made to create a timetable entry with the weekday parameter as {weekday}");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTypeNames = await _classTypeService.GetAllActiveClassTypeNames();

            if (classTypeNames == null)
            {
                _logger.Info($"There may be no class types created yet");
                return HttpNotFound();
            }

            var newClass = new AddClassTimetableViewModel(weekday, startHour, classTypeNames);

            return View(newClass);
        }

        // POST: ClassTimetable/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Create(AddClassTimetableViewModel viewModel)
        {
            if (viewModel == null)
            {
                _logger.Trace("Null parameter passed for Create ClassTimetable");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var isClassTimerangeValid = ValidateTime(viewModel.StartHour, viewModel.StartMinutes, viewModel.EndHour, viewModel.EndMinutes);
                if (!isClassTimerangeValid)
                {
                    _logger.Info($"An attempt was made to create a class timetable outside of the gym opening times");
                    viewModel.ClassTypeNames = await _classTypeService.GetAllActiveClassTypeNames();
                    return View(viewModel);
                }

                var isSuccess = await _classTimetableService.AddClassTimetable(viewModel);

                if (isSuccess == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        private bool ValidateTime(int startHour, int startMinutes, int endHour, int endMinutes)
        {
            if (startHour > endHour)
            {
                ModelState.AddModelError("", "Start hour should be lower than end hour");
                return false;
            }

            if ((startHour == endHour) && (startMinutes > endMinutes))
            {
                ModelState.AddModelError("", "Start time is before end time");
                return false;
            }

            if ((startHour == endHour) && (startMinutes == endMinutes))
            {
                ModelState.AddModelError("", "Start and end times should not be equal");
                return false;
            }

            var gymOpeningHour = Int32.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]);
            var gymClosingHour = Int32.Parse(ConfigurationManager.AppSettings["GymClosingHour"]);

            if (startHour < gymOpeningHour)
            {
                ModelState.AddModelError("", "Start time is before the gym opening time");
                return false;
            }

            if (endHour > gymClosingHour)
            {
                ModelState.AddModelError("", "End time is after gym closing time");
                return false;
            }

            if ((endHour == gymClosingHour) && (endMinutes != 0))
            {
                ModelState.AddModelError("", "End time is after gym closing time");
                return false;
            }

            return true;
        }

        // GET: ClassTimetable/Edit/5
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                _logger.Trace("No parameters passed for Edit");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTypeNames = await _classTypeService.GetAllActiveClassTypeNames();
            var classTimetable = await _classTimetableService.GetClassTimetable(id);

            if (classTimetable == null || classTypeNames == null)
            {
                _logger.Info($"Parameter {id} was passed for Edit but did not return a result");
                return HttpNotFound();
            }

            var viewModel = new EditClassTimetableViewModel(classTimetable, classTypeNames);

            return View(viewModel);
        }

        // POST: ClassTimetable/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Edit(EditClassTimetableViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isClassTimerangeValid = ValidateTime(viewModel.StartHour, viewModel.StartMinutes, viewModel.EndHour, viewModel.EndMinutes);
                if (!isClassTimerangeValid)
                {
                    viewModel.ClassTypeNames = await _classTypeService.GetAllActiveClassTypeNames();
                    return View(viewModel);
                }

                var isSuccess = await _classTimetableService.EditClassTimetable(viewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View(viewModel);
        }

        // GET: ClassTimetable/Delete/5
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                _logger.Trace("No parameters passed for Delete");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTimetable = await _classTimetableService.GetClassTimetable(id);

            if (classTimetable == null)
            {
                _logger.Info($"Parameter {id} was passed for Delete but did not return a result, therefore, no deletion occurred");
                return HttpNotFound();
            }

            return View(classTimetable);
        }

        // POST: ClassTimetable/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Delete(ClassTimetableDto dto)
        {
            var isSuccess = await _classTimetableService.DeleteClassTimetable(dto);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
