using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Runtime.Serialization;
namespace HotelManagementProject.Models

{
    public class Guest
    {
        [BsonId]

        
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        //public string Id { get; set; } 
        public string Guest_Name { get; set; }
        public string Guest_Email { get; set; }
        public string Guest_Phone { get; set; }
        public string Guest_Address { get; set; }
    }

}
