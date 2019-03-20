using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ScheduledClass;

namespace ProperArch01.WebApp.Controllers
{
    public class ScheduledClassController : Controller
    {
        private IScheduledClassService _scheduledClassService;

        public ScheduledClassController(IScheduledClassService scheduledClassService)
        {
            _scheduledClassService = scheduledClassService;
        }

        // GET: ScheduledClass
        public ActionResult Index()
        {
            var viewModel = _scheduledClassService.BuildIndexViewModel();
            return View(viewModel);
        }

        // GET: ScheduledClass/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = _scheduledClassService.GetScheduledClass(id);
            if (dto == null)
            {
                return HttpNotFound();
            }

            // replace this with service call to retrieve list of attendees
            var listOfAttendees = new List<string> { "Unpersisted attendee 1", "Unpersisted attendee 2", "Unpersisted attendee 3" };

            var viewModel = new DetailedScheduledClassViewModel(dto, listOfAttendees);

            return View(viewModel);
        }

        // GET: ScheduledClass/Create
        public ActionResult Create(string className, DateTime startTime)
        {
            var instructors = _scheduledClassService.GetAllInstructorNames();
            var viewModel = new CreateScheduledClassViewModel(className, startTime, instructors);

            return View(viewModel);
        }

        // POST: ScheduledClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateScheduledClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = _scheduledClassService.AddScheduledClass(viewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(viewModel);
        }

        // GET: ScheduledClass/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructors = _scheduledClassService.GetAllInstructorNames();

            ScheduledClassDto dto = _scheduledClassService.GetScheduledClass(id);

            if (dto == null)
            {
                return HttpNotFound();
            }

            EditScheduledClassViewModel viewModel = new EditScheduledClassViewModel(dto, instructors);

            return View(viewModel);
        }

        // POST: ScheduledClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditScheduledClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = _scheduledClassService.UpdateScheduledClass(viewModel);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        // GET: ScheduledClass/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ScheduledClassDto dto = _scheduledClassService.GetScheduledClass(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // POST: ScheduledClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var isSuccess = _scheduledClassService.DeleteScheduledClass(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
