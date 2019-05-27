using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Models.Holiday;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;
using System.Threading.Tasks;
using NLog;

namespace ProperArch01.WebApp.Controllers
{
    [Authorize(Roles = RoleNames.AdminName)]
    public class HolidayController : BaseController
    {
        new private readonly IHolidayService _holidayService;
        new private readonly IBaseService _baseService;

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public HolidayController(IHolidayService holidayService, IBaseService baseService) : base(baseService)
        {
            _holidayService = holidayService;
            _baseService = baseService;
        }

        // GET: Holiday
        public async Task<ActionResult> Index()
        {
            var holidays = await _holidayService.GetAllHolidays();
            return View(holidays);
        }

        // GET: Holiday/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                _logger.Trace("No parameters passed for Details");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var holiday = await _holidayService.GetHoliday(id);

            if (holiday == null)
            {
                _logger.Info($"Parameter {id} was passed for Details but did not return a result");
                return HttpNotFound();
            }
            return View(holiday);
        }

        // GET: Holiday/Create
        [Authorize(Roles = RoleNames.AdminName)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holiday/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Create(CreateHolidayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _holidayService.AddHoliday(viewModel);

                if (isSuccess)
                {
                    _logger.Info($"Holiday on {viewModel.HolidayDate} created successfully");
                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        // GET: Holiday/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                _logger.Trace("No parameters passed for Edit");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = await _holidayService.GetHoliday(id);

            if (dto == null)
            {
                _logger.Info($"Parameter {id} was passed for Edit but did not return a result");
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
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Edit(EditHolidayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _holidayService.EditHoliday(viewModel);
                if (isSuccess)
                {
                    _logger.Info($"Holiday ID {viewModel.Id} created successfully");
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        }

        // GET: Holiday/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                _logger.Trace("No parameters passed for Delete");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = await _holidayService.GetHoliday(id);
            if (dto == null)
            {
                _logger.Info($"Parameter {id} was passed for Delete but did not return a result, therefore, no deletion occurred");
                return HttpNotFound();
            }
            return View(dto);
        }

        // POST: Holiday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var isSuccess = await _holidayService.DeleteHoliday(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");
            }

            _logger.Info($"Holiday ID {id} has been successfully deleted");
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
