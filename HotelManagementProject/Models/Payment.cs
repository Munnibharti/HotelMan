namespace HotelManagementProject.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string ReservationId { get; set; }
    }
}
