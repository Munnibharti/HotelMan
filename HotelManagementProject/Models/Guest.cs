using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace HotelManagementProject.Models

{
    public class Guest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Guest_Name { get; set; }
        public string Guest_Email { get; set; }
        public string Guest_Phone { get; set; }
        public string Guest_Address { get; set; }
    }

}
