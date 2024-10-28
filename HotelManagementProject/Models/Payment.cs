using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManagementProject.Models
{
    public class Payment
    {
        [BsonId]


        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null;

        public string PaymentMethod { get; set; }

       
    }
}
