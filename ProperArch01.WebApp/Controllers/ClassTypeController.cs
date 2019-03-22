using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProperArch01.Persistence;
using ProperArch01.Persistence.EntityModels;
using ProperArch01.Contracts.Services;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.WebApp.Controllers
{
    [Authorize(Roles = RoleNames.AdminName)]
    public class ClassTypeController : Controller
    {
        private IClassTypeService _classTypeService;

        public ClassTypeController(IClassTypeService classTypeService)
        {
            _classTypeService = classTypeService;
        }

        // GET: ClassType
        public async Task<ActionResult> Index()
        {
            var results = await _classTypeService.GetAllClassTypes();

            return View(results);
        }

        // GET: ClassType/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classType = await _classTypeService.GetClassType(id);

            if (classType == null)
            {
                return HttpNotFound();
            }
            return View(classType);
        }

        // GET: ClassType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddClassTypeViewModel classType)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _classTypeService.AddClassType(classType);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            // need to populate the views. might be problems with conflicting get/post models per action

            return View(classType);
        }

        // GET: ClassType/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var classType = await _classTypeService.GetClassType(id);

            if (classType == null)
            {
                return HttpNotFound();
            }
            return View(classType);
        }

        // POST: ClassType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClassTypeDto classType)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _classTypeService.EditClassType(classType);

                if (!isSuccess)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ClassType/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classType = await _classTypeService.GetClassType(id);
            if (classType == null)
            {
                return HttpNotFound();
            }
            return View(classType);
        }

        // POST: ClassType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var isSuccess = await _classTypeService.DeleteClassType(id);

            if (!isSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }
    }
}
