using ProperArch01.Contracts.Models.Home;
using ProperArch01.Contracts.Services;
using System;
using System.Collections.Generic;
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

        public ActionResult Gallery()
        {
            ViewBag.Message = "Gallery";

            return View();
        }
    }
}