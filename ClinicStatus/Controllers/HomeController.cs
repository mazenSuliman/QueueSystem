using System;
using Microsoft.AspNetCore.Mvc;
using ClinicStatus.Services;
using ClinicStatus.ViewModels;
using ClinicStatus.Entities;
using Microsoft.AspNetCore.Http;

namespace ClinicStatus.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFloorData _Floors;
        private IClinicsData _Clinics;
        private IDoctorsData _Doctors;
        private ISession _session => _httpContextAccessor.HttpContext.Session;


        public HomeController(IFloorData floordata,
            IClinicsData clinicdata,
            IDoctorsData doctordata,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _Floors = floordata;
            _Clinics = clinicdata;
            _Doctors = doctordata;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Floors = _Floors.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Clinic(int id)
        {

            var model = new ClinicePageViewModel();

            model.Clinic = _Clinics.GetCliniByFloor(id);
            model.Floor = _Floors.Get(id);

            _session.SetString("floor", model.Floor.Name);

            return View(model);
        }

        [HttpGet]
        public IActionResult Doctor(int id)
        {
            var model = new DoctorPageViewModel();

            model.Doctors = _Doctors.GetDoctorByClinic(id, _session.GetString("floor"));
            model.clinic = _Clinics.Get(id);
            model.c = Convert.ToInt32(TempData["clinic"]);

            TempData["clinum"] = model.clinic.Speciality;

            return View(model);
        }

        public IActionResult Change(string id)
        {
            SqlConnect connect = new SqlConnect();

            connect.UpdateCommand(id);
            RedirectToActionResult redirectResult = new RedirectToActionResult("Doctor/" + TempData["clinum"], "Home", "");
            return redirectResult;
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
