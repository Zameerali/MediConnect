using MediConnectwithAuthentication.Models.Repositories;
using MediConnectwithAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using MediConnectwithAuthentication.Models.Interfaces;

namespace MediConnect.Controllers
{
    public class PatientController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public PatientController(IAppointmentRepository appointmentRepo)
        {
            _appointmentRepository = appointmentRepo;
        }
        public IActionResult Index()
        {
            return View("Dashboard");
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BookAppointment()
        {
            return View();
        }



        
        [HttpPost]
        public IActionResult BookAppointment(string DoctorName, DateTime AppointmentDate, string TimeSlot, string Reason)
        {
            Console.WriteLine("Reached BookAppointment POST");

           
            if (string.IsNullOrWhiteSpace(DoctorName) || string.IsNullOrWhiteSpace(TimeSlot) ||
                string.IsNullOrWhiteSpace(Reason) || AppointmentDate == default)
            {
                ViewBag.Message = "Error booking appointment. Please fill all fields.";
                return View(); 
            }

            
            Appointment newAppointment = new Appointment
            {
                DoctorName = DoctorName,
                AppointmentDate = AppointmentDate,
                TimeSlot = TimeSlot,
                Reason = Reason,
                Status = "Pending"
            };

            _appointmentRepository.AddAppointment(newAppointment);

            TempData["Message"] = "Appointment booked successfully!";
            return RedirectToAction("Appointments");
        }


        public IActionResult Appointments()
        {
            Console.WriteLine("Reached book");

            List<Appointment> appointments = _appointmentRepository.GetAppointments(); 
            return View("Appointments", appointments);
        }


        [HttpGet]
        public IActionResult EditAppointment(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View("EditAppointment", appointment); 
        }


        [HttpPost]
        public IActionResult EditAppointment(int id, string DoctorName, DateTime AppointmentDate, string TimeSlot, string Reason)
        {
            if (string.IsNullOrWhiteSpace(DoctorName) || string.IsNullOrWhiteSpace(TimeSlot) ||
                string.IsNullOrWhiteSpace(Reason) || AppointmentDate == default)
            {
                ViewBag.Message = "Please fill all fields.";
                var partialAppointment = new Appointment
                {
                    Id = id,
                    DoctorName = DoctorName,
                    AppointmentDate = AppointmentDate,
                    TimeSlot = TimeSlot,
                    Reason = Reason
                };
                return View("EditAppointment", partialAppointment);
            }

            var existingAppointment = _appointmentRepository.GetAppointmentById(id);

            if (existingAppointment == null)
            {
                return NotFound();
            }

            existingAppointment.DoctorName = DoctorName;
            existingAppointment.AppointmentDate = AppointmentDate;
            existingAppointment.TimeSlot = TimeSlot;
            existingAppointment.Reason = Reason;

            _appointmentRepository.UpdateAppointment(existingAppointment);

            TempData["Message"] = "Appointment updated successfully!";
            return RedirectToAction("Appointments");
        }


        [HttpGet]
        public IActionResult DeleteAppointment(int id)
        {
            _appointmentRepository.DeleteAppointment(id); 
            TempData["Message"] = "Appointment canceled successfully!";
            return RedirectToAction("Appointments");
        }


    }
}
