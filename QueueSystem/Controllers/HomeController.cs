using System;
using Microsoft.AspNetCore.Mvc;
using QueueSystem.Services;
using QueueSystem.ViewModels;
using QueueSystem.Entities;
using Microsoft.AspNetCore.Http;

namespace QueueSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFloorData _Floors;
        private IClinicsData _Clinics;
        private IDoctorsData _Doctors;
        private ITicketsData _Tickets;
        private ISession _session => _httpContextAccessor.HttpContext.Session;


        public HomeController(IFloorData floordata,
            IClinicsData clinicdata,
            IDoctorsData doctordata,
            ITicketsData ticketdata,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _Floors = floordata;
            _Clinics = clinicdata;
            _Doctors = doctordata;
            _Tickets = ticketdata;
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
            model.Clinic = _Clinics.Get(id);
            model.c = Convert.ToInt32(TempData["clinic"]);
            _session.SetString("clinic", model.Clinic.Name);

            return View(model);
        }

        [HttpGet]
        public IActionResult Ticket(int id)
        {

            Ticket tickety = new Ticket();

            tickety.DoctorCode = id;
            tickety.Doctor = _Doctors.GetDoctorName(id);
            tickety.Floor = _session.GetString("floor");
            tickety.Speciality = _session.GetString("clinic");
            tickety.Time = DateTime.Now;

            SqlConnect connect = new SqlConnect();
            connect.InsertCommand(tickety);

            tickety = _Tickets.Get(_session.GetString("floor"));

            if (tickety.Speciality == null)
            {

                Response.Redirect("/Home/Index");

            }


            return View(tickety);

        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
