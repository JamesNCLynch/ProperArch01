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
using System.IO;
using System.Configuration;

namespace ProperArch01.WebApp.Controllers
{
    public class ClassTypeController : BaseController
    {
        new private readonly IClassTypeService _classTypeService;
        new private readonly IBaseService _baseService;

        public ClassTypeController(IClassTypeService classTypeService, IBaseService baseService) : base(baseService)
        {
            _classTypeService = classTypeService;
            _baseService = baseService;
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

            var classType = await _classTypeService.BuildClassTypeViewModel(id);

            if (classType == null)
            {
                return HttpNotFound();
            }

            return View(classType);
        }

        // GET: ClassType/Create
        [Authorize(Roles = RoleNames.AdminName)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Create(AddClassTypeViewModel classType)
        {
            if (ModelState.IsValid)
            {
                // Verify that the user selected a file
                if (classType.ImageFile != null && classType.ImageFile.ContentLength > 0)
                {
                    if (classType.ImageFile.ContentLength > 20000000)
                    {
                        ModelState.AddModelError("", "Please ensure image file is less than 2MB");
                        return RedirectToAction("Create");
                    }

                    // extract only the filename
                    var fileName = Path.GetFileName(classType.ImageFile.FileName);

                    if (!fileName.Contains(".jpg") && !fileName.Contains(".png"))
                    {
                        ModelState.AddModelError("", "Please ensure file is in JPG or PNG format");
                        return RedirectToAction("Create");
                    }

                    if (fileName.Contains(" "))
                    {
                        ModelState.AddModelError("", "Image filename cannot contain spaces or special characters");
                        return RedirectToAction("Create");
                    }

                    var filePath = ConfigurationManager.AppSettings["ClassTypeAssetPath"];

                    // store the file inside ~/App_Data/classtype folder
                    var path = Path.Combine(Server.MapPath(filePath), fileName);
                    classType.ImageFile.SaveAs(path);

                    classType.ImageFileName = fileName;
                }

                // have not populated view yet - need to do that and then test

                var isSuccess = await _classTypeService.AddClassType(classType);

                if (isSuccess)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(classType);
        }

        // GET: ClassType/Edit/5
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dto = await _classTypeService.GetClassType(id);

            if (dto == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditClassTypeViewModel(dto);

            return View(viewModel);
        }

        // POST: ClassType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminName)]
        public async Task<ActionResult> Edit(EditClassTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null && viewModel.ImageFile.ContentLength > 0)
                {
                    if (viewModel.ImageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("", "Please ensure image file is less than 2MB");
                        return RedirectToAction("Edit");
                    }

                    var fileName = Path.GetFileName(viewModel.ImageFile.FileName);

                    if (!fileName.Contains(".jpg") && !fileName.Contains(".png"))
                    {
                        ModelState.AddModelError("", "Please ensure file is in JPG or PNG format");
                        return RedirectToAction("Edit");
                    }

                    if (fileName.Contains(" "))
                    {
                        ModelState.AddModelError("", "Image filename cannot contain spaces or special characters");
                        return RedirectToAction("Edit");
                    }

                    // store the file inside ~/App_Data/classtype folder
                    var filePath = ConfigurationManager.AppSettings["ClassTypeAssetPath"];
                    var path = Path.Combine(Server.MapPath(filePath), fileName);
                    viewModel.ImageFile.SaveAs(path);

                    viewModel.ImageFileName = fileName;
                }
                   

                var isSuccess = await _classTypeService.EditClassType(viewModel);

                if (!isSuccess)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return RedirectToAction("Details", "ClassType", new { Id = viewModel.Id});
        }

        // GET: ClassType/Delete/5
        [Authorize(Roles = RoleNames.AdminName)]
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
        [Authorize(Roles = RoleNames.AdminName)]
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
