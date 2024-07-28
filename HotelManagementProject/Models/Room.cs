using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotelManagementProject.Models
{
    public class Room
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Room_Number { get; set; }
        public string Room_Type { get; set; }
        public Decimal Room_Price { get; set; }
        public bool Room_Status { get; set; }

        
    }
}
