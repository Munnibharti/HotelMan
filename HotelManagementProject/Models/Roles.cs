using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementProject.Models
{
    public class Roles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        public string userName { get; set; }

        public string Password { get; set; }
       
        public string RoleType { get; set; }  // Possible values: "Owner", "Manager", "Receptionist"


    }
}
