using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetReservationAsync();
            return View(reservations);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _reservationService.CreatereservationAsync(reservation);
                return RedirectToAction("Index");
            }
            return View(reservation);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var guest = await _reservationService.GetReservationByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,GuestId,RoomId,CheckInDate,CheckOutDate,TotalAmount,PaymentId")] Reservation reservationDetails)
        {
            if (id != reservationDetails.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _reservationService.UpdateReservationAsync(id, reservationDetails);
                return RedirectToAction(nameof(Index));
            }

            return View(reservationDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var guest = await _reservationService.GetReservationByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
