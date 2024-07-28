using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
