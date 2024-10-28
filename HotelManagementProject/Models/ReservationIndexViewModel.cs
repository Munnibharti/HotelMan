namespace HotelManagementProject.Models
{
    public class ReservationIndexViewModel
    {
        public string Id { get; set; }
        public string Guest_Email { get; set; }
        public string Room_Number { get; set; }
        public string Payment_Method { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
