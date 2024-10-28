using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HotelManagementProject.Controllers
{
    public class GuestController:Controller
    {
        private readonly IGuestServices _guestService;

        public GuestController(IGuestServices guestServices)
        {
            _guestService = guestServices;
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
        public async Task<IActionResult> Edit(ObjectId id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("Id,Guest_Name,Guest_Email,Guest_Phone,Guest_Address")] Guest guestDetail)
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

        // GET: Guests/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: Guests/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            await _guestService.DeleteGuestAsync(id);  // Deletes the guest
            return RedirectToAction(nameof(Index));  // Redirects to the guest list
        }


    }
}
