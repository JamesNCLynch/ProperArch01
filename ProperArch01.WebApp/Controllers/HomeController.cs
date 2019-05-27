using NLog;
using ProperArch01.Contracts.Models.Home;
using ProperArch01.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProperArch01.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        new private readonly IBaseService _baseService;
        private readonly IHomeService _homeService;

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(IHomeService homeService, IBaseService baseService) : base(baseService)
        {
            _baseService = baseService;
            _homeService = homeService;
        }

        public async Task<ActionResult> Index()
        {
            HomeIndexViewModel viewModel = await _homeService.BuildIndexViewModel();
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MissionAim()
        {
            ViewBag.Message = "Mission Aim";

            return View();
        }

        public ActionResult History()
        {
            ViewBag.Message = "History";

            return View();
        }

        public ActionResult MembershipRates()
        {
            ViewBag.Message = "Membership Rates";

            return View();
        }

        public ActionResult CorporateRates()
        {
            ViewBag.Message = "Corporate Rates";

            return View();
        }

        public ActionResult Payg()
        {
            ViewBag.Message = "Pay as you go";

            return View();
        }

        public ActionResult RoomFacilityRates()
        {
            ViewBag.Message = "Room/Facility Rates";

            return View();
        }

        public ActionResult BookingForms()
        {
            ViewBag.Message = "Booking Forms";

            return View();
        }

        public ActionResult SportsHall()
        {
            ViewBag.Message = "Sports Hall";

            return View();
        }

        public ActionResult Gym()
        {
            ViewBag.Message = "Gym";

            return View();
        }

        public ActionResult Activities()
        {
            ViewBag.Message = "Activities";

            return View();
        }

        public ActionResult Pitch()
        {
            ViewBag.Message = "Pitch";

            return View();
        }

        public ActionResult MeetingRooms()
        {
            ViewBag.Message = "Meeting Rooms";

            return View();
        }

        public ActionResult SummerCamps()
        {
            ViewBag.Message = "Summer camps";

            return View();
        }

        public ActionResult Birthday()
        {
            ViewBag.Message = "Birthday";

            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Gallery()
        {
            ViewBag.Message = "Gallery";

            var viewModel = new GalleryViewModel()
            {
                GalleryFileList = await _homeService.GetListOfGalleryFiles()
            };

            return View(viewModel);
        }

        //Single File Upload
        [HttpPost]
        public async Task<ActionResult> Gallery(GalleryViewModel viewModel)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {
                var galleryFilePath = ConfigurationManager.AppSettings["GalleryAssetPath"];

                //iterating through multiple file collection   
                foreach (var file in viewModel.FileUploadModel.Files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var inputFileName = Path.GetFileName(file.FileName);

                        if (inputFileName.Contains(" "))
                        {
                            ModelState.AddModelError("", "File names cannot contain spaces");
                            return RedirectToAction("Gallery");
                        }

                        var savePath = Path.Combine(Server.MapPath(galleryFilePath) + inputFileName);

                        //Save file to server folder  
                        file.SaveAs(savePath);
                    }
                }

                _logger.Info($"{viewModel.FileUploadModel.Files.Count()} files uploaded to Gallery view");

                //assigning file uploaded status to ViewBag for showing message to user.  
                ViewBag.UploadStatus = viewModel.FileUploadModel.Files.Count().ToString() + " files uploaded successfully.";
            }

            // update view with latest uploads
            viewModel.GalleryFileList = await _homeService.GetListOfGalleryFiles();

            return View(viewModel);
        }
    }
}