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
using System.Threading.Tasks;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.WebApp.Controllers
{
    [Authorize(Roles = RoleNames.AdminName)]
    public class ScheduledClassController : Controller
    {
        private IScheduledClassService _scheduledClassService;

        public ScheduledClassController(IScheduledClassService scheduledClassService)
        {
            _scheduledClassService = scheduledClassService;
        }

        // GET: ScheduledClass
        public async Task<ActionResult> Index()
        {
            var viewModel = await _scheduledClassService.BuildIndexViewModel();
            return View(viewModel);
        }

        // GET: ScheduledClass/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = await _scheduledClassService.GetScheduledClass(id);
            if (dto == null)
            {
                return HttpNotFound();
            }

            var viewModel = await _scheduledClassService.BuildScheduledClassDetailsViewModel(id);

            return View(viewModel);
        }

        // GET: ScheduledClass/Create
        public async Task<ActionResult> Create(string className, DateTime startTime)
        {
            var instructors = await _scheduledClassService.GetAllInstructorNames();
            var viewModel = new CreateScheduledClassViewModel(className, startTime, instructors);

            return View(viewModel);
        }

        // POST: ScheduledClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateScheduledClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _scheduledClassService.AddScheduledClass(viewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(viewModel);
        }

        // GET: ScheduledClass/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructors = await _scheduledClassService.GetAllInstructorNames();

            ScheduledClassDto dto = await _scheduledClassService.GetScheduledClass(id);

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
        public async Task<ActionResult> Edit(EditScheduledClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _scheduledClassService.UpdateScheduledClass(viewModel);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        // GET: ScheduledClass/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ScheduledClassDto dto = await _scheduledClassService.GetScheduledClass(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // POST: ScheduledClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var isSuccess = await _scheduledClassService.DeleteScheduledClass(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
