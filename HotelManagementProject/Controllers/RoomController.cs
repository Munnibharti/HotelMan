using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService) 
        { 
        _roomService = roomService;
        }
        public async  Task<IActionResult> Index()
        {
           var room = await _roomService.GetRoomAsync();
            return View(room);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(room);
                return RedirectToAction("Index");
            }
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Room_Number,Room_Type,Room_Price,Room_Status")] Room roomDetails)
        {
            if (id != roomDetails.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _roomService.UpdateRoomAsync(id, roomDetails);
                return RedirectToAction(nameof(Index));
            }

            return View(roomDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var guest = await _roomService.GetRoomByIdAsync(Id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _roomService.DeleteRoomAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}


