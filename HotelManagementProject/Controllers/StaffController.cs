using Microsoft.AspNetCore.Mvc;
using HotelManagementProject.Models;
using HotelManagementProject.Service;
using MongoDB.Bson;

namespace HotelManagementProject.Controllers
{
    public class StaffController : Controller
    {
        public readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        public async Task<IActionResult> Index()
        {
            var staffs = await _staffService.GetStaffsAsync();
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
                await _staffService.CreateStaffAsync(staff);
                return RedirectToAction("Index");
            }
            return View(staff);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(ObjectId id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("Id,user_Name,Password,Position,salary")] Staff staffDetails)
        {
            if (id != staffDetails.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _staffService.UpdateStaffAsync(id, staffDetails);
                return RedirectToAction(nameof(Index));
            }

            return View(staffDetails);
        }
        [HttpGet]
       public async Task<IActionResult> Delete(ObjectId id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if(staff==null)
            {
                return NotFound();
            }
            return View(staff);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            await _staffService.DeleteStaffAsync(id);
            return RedirectToAction(nameof(Index));
        }

    
}
}
