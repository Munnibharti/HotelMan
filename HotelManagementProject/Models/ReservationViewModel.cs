using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementProject.Models
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<SelectListItem> GuestEmails { get; set; }
        public List<SelectListItem> RoomNumbers { get; set; }
        public List<SelectListItem> PaymentMethods { get; set; }
    }
}
