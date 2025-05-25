using Microsoft.AspNetCore.Mvc;

namespace MediConnect.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View("Dashboard");
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult ManageAppointments()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Schedule()
        {
            return View();
        }
    }
}
