using HotelManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace HotelManagementProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMongoCollection<Roles> _roleCollection;

        public HomeController(IMongoDatabase database)
        {
            _roleCollection = database.GetCollection<Roles>("Roles");
        }

        // GET: Login Page
        public IActionResult Login()
        {
            return View(new LoginView());
        }

        // POST: Login Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginView model)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<Roles>.Filter.Eq(g => g.userName, model.userName) &
                             Builders<Roles>.Filter.Eq(g => g.Password, model.Password) &
                             Builders<Roles>.Filter.Eq(g => g.RoleType, model.RoleType);
                var user = _roleCollection.Find(filter).FirstOrDefault();

                if (user != null)
                {
                    // Set session variables
                    HttpContext.Session.SetString("UserName", user.userName);
                    HttpContext.Session.SetString("RoleType", user.RoleType);

                    // Redirect to dashboard based on role type
                    return RedirectToAction("Dashboard", new { roleType = user.RoleType });
                }
                // If user is not found, add a model error
                ModelState.AddModelError("", "Invalid username, password, or role type.");
            }
            return View(model);
        }

        // GET: Dashboard Page
        public IActionResult Dashboard(string roleType)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(roleType))
            {
                ViewBag.UserName = userName;

                // Redirect to specific dashboard based on role type
                switch (roleType)
                {
                    case "Owner":
                        return View("OwnerDashboard");
                    case "Manager":
                        return View("ManagerDashboard");
                    case "Receptionist":
                        return View("ReceptionistDashboard");
                    default:
                        return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
