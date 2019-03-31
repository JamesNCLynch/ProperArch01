using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.ClassAttendance;
using ProperArch01.Contracts.Constants;
using Microsoft.AspNet.Identity;

namespace ProperArch01.WebApp.Controllers
{
    public class ClassAttendanceController : Controller
    {
        private IClassAttendanceService _classAttendanceService;

        public ClassAttendanceController(IClassAttendanceService classAttendanceService) {
            _classAttendanceService = classAttendanceService;
        }

        // GET: ClassAttendance
        public async Task<ActionResult> Index()
        {
            var attendeeId = User.Identity.GetUserId();

            var viewModel = await _classAttendanceService.BuildClassAttendanceIndexViewModel(attendeeId);
            return View(viewModel);
        }

        // GET: ClassAttendance/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dto = await _classAttendanceService.GetClassAttendance(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // GET: ClassAttendance/Create
        public async Task<ActionResult> Create(string scid)
        {
            if (scid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduledClass = await _classAttendanceService.GetScheduledClass(scid);

            var viewModel = new CreateClassAttendanceViewModel(scheduledClass);
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateClassAttendanceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.AttendeeId = User.Identity.GetUserId();

                var response = await _classAttendanceService.AddClassAttendance(viewModel);

                switch (response)
                {
                    case ClassAttendanceResponse.Success:
                        return RedirectToAction("Index");
                    case ClassAttendanceResponse.ClassCancelled:
                        ModelState.AddModelError("", "Class has been cancelled. Sorry!");
                        break;
                    case ClassAttendanceResponse.ClassNotFound:
                        ModelState.AddModelError("", "Class not found. Please try again");
                        break;
                    case ClassAttendanceResponse.UnspecifiedError:
                        ModelState.AddModelError("", "Something has gone wrong. Please refresh the page and try again");
                        break;
                    default:
                        ModelState.AddModelError("", "Error");
                        break;
                }
            }

            return View(viewModel);
        }

        // GET: ClassAttendance/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dto = await _classAttendanceService.GetClassAttendance(id);
            if (dto == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditClassAttendanceViewModel(dto);

            return View(viewModel);
        }

        // POST: ClassAttendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditClassAttendanceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _classAttendanceService.EditClassAttendance(viewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        // GET: ClassAttendance/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dto = await _classAttendanceService.GetClassAttendance(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // POST: ClassAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            bool isSuccess = await _classAttendanceService.DeleteClassAttendance(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
