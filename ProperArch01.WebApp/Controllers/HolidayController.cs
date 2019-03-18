using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProperArch01.Contracts.Services;
using ProperArch01.WebApp.Models;
using ProperArch01.Contracts.Models.Holiday;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.WebApp.Controllers
{
    public class HolidayController : Controller
    {
        private IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        // GET: Holiday
        public ActionResult Index()
        {
            var holidays = _holidayService.GetAllHolidays();
            return View(holidays);
        }

        // GET: Holiday/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var holiday = _holidayService.GetHoliday(id);

            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // GET: Holiday/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holiday/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateHolidayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = _holidayService.AddHoliday(viewModel);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        // GET: Holiday/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = _holidayService.GetHoliday(id);

            if (dto == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditHolidayViewModel(dto);
            return View(viewModel);
        }

        // POST: Holiday/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditHolidayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = _holidayService.EditHoliday(viewModel);
                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        }

        // GET: Holiday/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = _holidayService.GetHoliday(id);
            if (dto == null)
            {
                return HttpNotFound();
            }
            return View(dto);
        }

        // POST: Holiday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var isSuccess = _holidayService.DeleteHoliday(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
