using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
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


    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var staff = await IStaffService.GetStaffByIdAsync(id);
        if (staff == null)
        {
            return NotFound();
        }
        return View(staff);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Staff_Name,Position,Salary")] Staff staffDetails)
    {
        if (id != staffDetails.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await _guestService.UpdateGuestAsync(id, guestDetails);
            return RedirectToAction(nameof(Index));
        }

        return View(guestDetails);
    }

}
