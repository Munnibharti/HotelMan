using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    
    public class StaffController : Controller
    {
        private readonly IStaffService _staffServices;

        public StaffController(IStaffService staffServices)
        {
            _staffServices = staffServices;
        }
        public async Task<IActionResult> Index()
        {
           var staffs = await _staffServices.GetStaffsAsync();
            return View(staffs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                await _staffServices.CreateStaffAsync(staff);
                return RedirectToAction("Index");
            }
            return View(staff);
        }

    }


    //[HttpGet]
    //public async Task<IActionResult> Edit(string id)
    //{
    //    var staff = await _staffServices.GetStaffByIdAsync(id);
    //    if (staff == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(staff);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Edit(string id, [Bind("Id,Staff_Name,Position,Salary")] Staff staffDetails)
    //{
    //    if (id != staffDetails.Id)
    //    {
    //        return BadRequest();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        await _guestService.UpdateGuestAsync(id, guestDetails);
    //        return RedirectToAction(nameof(Index));
    //    }

    //    return View(guestDetails);
    //}

}
