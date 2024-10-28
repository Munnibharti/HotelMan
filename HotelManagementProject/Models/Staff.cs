using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Policy;

namespace HotelManagementProject.Models
{
    public class Staff
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } 

        public string user_Name { get; set; }

        public string Password { get; set; }

        public string Position { get; set; }

       public decimal salary { get; set; } 
    }
}
