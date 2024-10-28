using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
namespace HotelManagementProject.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public ObjectId Id { get; set; } 
        public string GuestEmail { get; set; }
        public string RoomNumber { get; set; }
        public string PaymentMethod { get; set; }


        public DateTime CheckInDate { get; set; }

        [CheckOutDateValidation("CheckInDate", ErrorMessage = "Check-out date must be greater than the check-in date.")]
        public DateTime CheckOutDate { get; set; }

        public decimal TotalAmount { get; set; }

       
        // Navigation properties (optional if you want to access related data directly)
        //public Guest Guest { get; set; }
        //public Room Room { get; set; }
        //public Payment Payment { get; set; }
    }

    public class CheckOutDateValidationAttribute : ValidationAttribute
    {
        private readonly string _checkInDatePropertyName;

        public CheckOutDateValidationAttribute(string checkInDatePropertyName)
        {
            _checkInDatePropertyName = checkInDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var checkOutDate = (DateTime)value;

            // Get the CheckInDate value from the context
            var propertyInfo = validationContext.ObjectType.GetProperty(_checkInDatePropertyName);
            if (propertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {_checkInDatePropertyName}");
            }

            var checkInDate = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance, null);

            // Validation logic: check if CheckOutDate is greater than CheckInDate
            if (checkOutDate <= checkInDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
