using MediConnectwithAuthentication.Models.Repositories;
using MediConnectwithAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using MediConnectwithAuthentication.Models.Interfaces;
using Microsoft.AspNetCore.SignalR;
using MediConnectwithAuthentication.Hubs;   // Your SignalR hub namespace

namespace MediConnect.Controllers
{
    public class PatientController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHubContext<AppointmentsHub> _hubContext;  // Inject SignalR HubContext

        public PatientController(IAppointmentRepository appointmentRepo, IHubContext<AppointmentsHub> hubContext)
        {
            _appointmentRepository = appointmentRepo;
            _hubContext = hubContext;
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
        public async Task<IActionResult> BookAppointment(string DoctorName, DateTime AppointmentDate, string TimeSlot, string Reason)
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

            // Notify all clients about the new appointment
            await _hubContext.Clients.All.SendAsync("ReceiveAppointmentUpdate", "Added", newAppointment);

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
        public async Task<IActionResult> EditAppointment(int id, string DoctorName, DateTime AppointmentDate, string TimeSlot, string Reason)
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

            // Notify all clients about the updated appointment
            await _hubContext.Clients.All.SendAsync("ReceiveAppointmentUpdate", "Updated", existingAppointment);

            TempData["Message"] = "Appointment updated successfully!";
            return RedirectToAction("Appointments");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            _appointmentRepository.DeleteAppointment(id);

            // Notify all clients about the deleted appointment by sending id only
            await _hubContext.Clients.All.SendAsync("ReceiveAppointmentUpdate", "Deleted", id);

            TempData["Message"] = "Appointment canceled successfully!";
            return RedirectToAction("Appointments");
        }
    }
}
