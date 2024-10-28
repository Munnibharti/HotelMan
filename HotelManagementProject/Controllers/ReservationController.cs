using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using MongoDB.Bson;

namespace HotelManagementProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IEmailServices _emailService;
        private readonly IReservationService _reservationService;
        private readonly IGuestServices _guestServices;
        private readonly IRoomService _roomService;
        private readonly IPaymentService _paymentService;
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Guest> _guestCollection;
        private readonly IMongoCollection<Room> _roomCollection;
        private readonly IMongoCollection<Payment> _paymentCollection;
       


        public ReservationController(IReservationService reservationService, IEmailServices emailService, IGuestServices guestServices, IRoomService roomService, IPaymentService paymentService,IMongoDatabase database)
        {
            _reservationService = reservationService;
            _emailService = emailService;
            _guestServices = guestServices;
            _roomService = roomService;
            _paymentService = paymentService;
            _reservationCollection = database.GetCollection<Reservation>("Reservations");
            _guestCollection = database.GetCollection<Guest>("Guests");
            _roomCollection = database.GetCollection<Room>("Rooms");
            _paymentCollection = database.GetCollection<Payment>("Payments");
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationCollection.Find(_ => true).ToListAsync();
            return View(reservations);
            
        }

        [HttpGet]
        public async Task<IActionResult> Create()

        {  
            var guests = await _guestCollection.Find(_ => true).ToListAsync();
            //var rooms = await _roomCollection.Find(_ => true).ToListAsync();
            // Filter only rooms where Active is true
            var rooms = await _roomCollection.Find(room => room.Room_Status == true).ToListAsync();
            var viewModel = new ReservationViewModel
            {
                GuestEmails = guests.Select(g => new SelectListItem
                {
                    Text = g.Guest_Email,
                    Value = g.Guest_Email
                }).ToList(),
                RoomNumbers = rooms.Select(g => new SelectListItem
                {
                    Text = g.Room_Number.ToString(),
                    Value = g.Room_Number.ToString()
                }).ToList(),
                PaymentMethods = new List<SelectListItem>
        {
            new SelectListItem { Text = "Credit Card", Value = "Credit Card" },
            new SelectListItem { Text = "Cash", Value = "Cash" }
        }
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReservationViewModel viewModel)
        {
            
            // Manually validate CheckOutDate
            // Manually validate CheckOutDate
            if (viewModel.Reservation.CheckOutDate <= viewModel.Reservation.CheckInDate)
            {
                ModelState.AddModelError("viewModel.Reservation.CheckOutDate", "Check-out date must be greater than the check-in date.");
            }

            // Always insert the reservation regardless of ModelState validity
            
            await _reservationService.CreatereservationAsync(viewModel.Reservation);
           // await _reservationCollection.InsertOneAsync(viewModel.Reservation);

            // Redirect to the SendEmail action to open Outlook with the guest email
            return RedirectToAction("SendEmail", new { guestEmail = viewModel.Reservation.GuestEmail,
            checkInDate = viewModel.Reservation.CheckInDate,
            checkOutDate = viewModel.Reservation.CheckOutDate,
            roomNumber = viewModel.Reservation.RoomNumber,
            PaymentMethod = viewModel.Reservation.PaymentMethod,
            totalAmount = viewModel.Reservation.TotalAmount,
            
            });
        }
        //public ActionResult Mailto()
        //{
        //    // Use TempData to pass the mailto URL to the View
        //    var mailtoUrl = TempData["MailtoUrl"] as string;
        //    ViewBag.MailtoUrl = mailtoUrl;

        //    return View();
        //}
        public ActionResult SendEmail(string guestEmail, DateTime checkInDate, DateTime checkOutDate, decimal totalAmount, string roomNumber,string PaymentMethod)
{
            // Check if the guest email is provided
            if (string.IsNullOrEmpty(guestEmail))
            {
                // If no guest email is provided, redirect back to the Index page
                return RedirectToAction("Index");
            }

            // Pass the guestEmail to the view via ViewBag
            ViewBag.GuestEmail = guestEmail;
            ViewBag.CheckInDate = checkInDate.ToString("yyyy-MM-dd");
            ViewBag.CheckOutDate = checkOutDate.ToString("yyyy-MM-dd");
            ViewBag.TotalAmount = totalAmount.ToString("F2"); 
            ViewBag.RoomNumber = roomNumber;
            ViewBag.PaymentMethod = PaymentMethod;


            // Return the SendEmail view
            return View();
        }
        private async Task<IEnumerable<SelectListItem>> GetGuestSelectList()
        {
            var guests = await _guestCollection.Find(_ => true).ToListAsync();
            return guests.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Guest_Email
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetRoomSelectList()
        {
            var rooms = await _roomCollection.Find(_ => true).ToListAsync();
            return rooms.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Room_Number.ToString(),
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetPaymentSelectList()
        {
            var payments = await _paymentCollection.Find(_ => true).ToListAsync();
            return payments.Select(p => new SelectListItem
            {
                Value = p.Id,
                Text = p.PaymentMethod
            });
        }






        [HttpGet]
            public async Task<IActionResult> GetGuestEmail(ObjectId guestId)
            {
                var guest = await _guestServices.GetGuestByIdAsync(guestId);
                if (guest != null)
                {
                    return Json(new { guestEmail = guest.Guest_Email });
                }
                return Json(new { guestEmail = "" });
            }

            

            [HttpGet]
            public async Task<IActionResult> Edit(string id)
            {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservationByIdAsync(objectId);
            if (reservation == null)
            {
                return NotFound();
            }

            // Populate dropdowns for guests and rooms
            var guests = await _guestServices.GetGuestsAsync();
            var rooms = await _roomService.GetRoomAsync();

            ViewBag.GuestEmails = guests.Select(g => new SelectListItem
            {
                Value = g.Guest_Email,
                Text = g.Guest_Email,
                Selected = g.Guest_Email == reservation.GuestEmail
            }).ToList();

            ViewBag.RoomNumbers = rooms.Select(r => new SelectListItem
            {
                Value = r.Room_Number.ToString(),
                Text = r.Room_Number.ToString(),
                Selected = r.Room_Number.ToString() == reservation.RoomNumber
            }).ToList();

            // Hard-coded payment methods
            ViewBag.Payments = new List<SelectListItem>
    {
        new SelectListItem { Value = "Cash", Text = "Cash", Selected = reservation.PaymentMethod == "Cash" },
        new SelectListItem { Value = "Credit Card", Text = "Credit Card", Selected = reservation.PaymentMethod == "Credit Card" }
    };

            return View(reservation);
        }

            [HttpPost]
            public async Task<IActionResult> Edit(string id, [Bind("Id,GuestEmail,RoomNumber,CheckInDate,CheckOutDate,TotalAmount,PaymentMethod")] Reservation reservationDetails)
            {

            if (!ModelState.IsValid)
            {
                // Re-populate dropdowns if model state is invalid
                var guests = await _guestServices.GetGuestsAsync();
                var rooms = await _roomService.GetRoomAsync();

                ViewBag.GuestEmails = guests.Select(g => new SelectListItem
                {
                    Value = g.Guest_Email,
                    Text = g.Guest_Email,
                    Selected = g.Guest_Email == reservationDetails.GuestEmail
                }).ToList();

                ViewBag.RoomNumbers = rooms.Select(r => new SelectListItem
                {
                    Value = r.Room_Number.ToString(),
                    Text = r.Room_Number.ToString(),
                    Selected = r.Room_Number.ToString() == reservationDetails.RoomNumber
                }).ToList();

                ViewBag.Payments = new List<SelectListItem>
        {
            new SelectListItem { Value = "Cash", Text = "Cash", Selected = reservationDetails.PaymentMethod == "Cash" },
            new SelectListItem { Value = "Credit Card", Text = "Credit Card", Selected = reservationDetails.PaymentMethod == "Credit Card" }
        };

                return View(reservationDetails);
            }

            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservationByIdAsync(objectId);
            if (reservation == null)
            {
                return NotFound();
            }

            // Update the reservation
            await _reservationService.UpdateReservationAsync(objectId, reservationDetails);

            return RedirectToAction("Index");
        }

            [HttpGet]
            public async Task<IActionResult> Delete(ObjectId id)
            {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(ObjectId id)
            {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
        }
    }

