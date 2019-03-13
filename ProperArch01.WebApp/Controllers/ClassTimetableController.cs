using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using ProperArch01.Persistence;
using ProperArch01.Persistence.EntityModels;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.WebApp.Controllers
{
    public class ClassTimetableController : Controller
    {
        private IClassTimetableService _classTimetableService;
        private IClassTypeService _classTypeService;

        public ClassTimetableController(IClassTimetableService classTimetableService, IClassTypeService classTypeService)
        {
            _classTimetableService = classTimetableService;
            _classTypeService = classTypeService;
        }

        // GET: ClassTimetable
        public ActionResult Index()
        {
            //var results = _classTimetableService.GetClassTimetables();
            var classTimetableViewModel = _classTimetableService.BuildTimetableViewModel();
            return View(classTimetableViewModel);
        }

        // GET: ClassTimetable/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTimetable = _classTimetableService.GetClassTimetable(id);

            if (classTimetable == null)
            {
                return HttpNotFound();
            }

            return View(classTimetable);
        }

        // GET: ClassTimetable/Create
        public ActionResult Create(int weekday, int startHour)
        {
            if (weekday < 0 || weekday > 7)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTypeNames = _classTypeService.GetAllActiveClassTypeNames();

            var newClass = new AddClassTimetableModel(weekday, startHour, classTypeNames);

            return View(newClass);
        }

        // POST: ClassTimetable/Create
        [HttpPost]
        public ActionResult Create(AddClassTimetableModel viewModel)
        {
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var isSuccess = _classTimetableService.AddClassTimetable(viewModel);

                if (isSuccess == true)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        // GET: ClassTimetable/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTypeNames = _classTypeService.GetAllActiveClassTypeNames();
            var classTimetable = _classTimetableService.GetClassTimetable(id);

            if (classTimetable == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditClassTimetableModel(classTimetable, classTypeNames);

            return View(viewModel);
        }

        // POST: ClassTimetable/Edit/5
        [HttpPost]
        public ActionResult Edit(EditClassTimetableModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = _classTimetableService.EditClassTimetable(viewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View(viewModel);
        }

        // GET: ClassTimetable/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classTimetable = _classTimetableService.GetClassTimetable(id);

            if (classTimetable == null)
            {
                return HttpNotFound();
            }

            return View(classTimetable);
        }

        // POST: ClassTimetable/Delete/5
        [HttpPost]
        public ActionResult Delete(ClassTimetableDto dto)
        {
            var isSuccess = _classTimetableService.DeleteClassTimetable(dto);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
