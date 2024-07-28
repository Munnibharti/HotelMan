using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    public class GuestController:Controller
    {
        private readonly IGuestServices _guestService;

        public GuestController(IGuestServices guestService)
        {
            _guestService = guestService;
        }

        public async Task<IActionResult> Index()
        {
            var guests = await _guestService.GetGuestsAsync();
            return View(guests);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Guest guests)
        {
            if (ModelState.IsValid)
            {
                await _guestService.CreateGuestAsync(guests);
                return RedirectToAction("Index");
            }
            return View(guests);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Guest_Name,Guest_Email,Guest_Phone,Guest_Address")] Guest guestDetail)
        {
            if (id != guestDetail.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _guestService.UpdateGuestAsync(id, guestDetail);
                return RedirectToAction(nameof(Index));
            }

            return View(guestDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _guestService.DeleteGuestAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
