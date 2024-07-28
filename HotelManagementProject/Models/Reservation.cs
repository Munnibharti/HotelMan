using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace HotelManagementProject.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string GuestId { get; set; }
        public string RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentId { get; set; }
    }

}

