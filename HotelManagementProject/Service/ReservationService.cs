using HotelManagementProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotelManagementProject.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Guest> _guestCollection;
        private readonly IMongoCollection<Payment> _paymentCollection;
        private readonly IMongoCollection<Room> _roomCollection;
        public ReservationService(IMongoDatabase database)
        {
            _reservationCollection = database.GetCollection<Reservation>("Reservations");
            _guestCollection = database.GetCollection<Guest>("Guests");
            _paymentCollection = database.GetCollection<Payment>("Payments");
            _roomCollection = database.GetCollection<Room>("Rooms");
        }
        public async Task<List<Reservation>> GetReservationAsync()
        {
            return await _reservationCollection.Find(_ => true).ToListAsync();
        } 
        
        
        public async Task CreatereservationAsync(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }
            await _reservationCollection.InsertOneAsync(reservation);
        }
       

        public async Task<Reservation> GetReservationByIdAsync(ObjectId id)

        {
            return await _reservationCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }


        public async Task DeleteReservationAsync(ObjectId id)
        {

            try
            {
                
                

                // Create a filter for the ObjectId
                var filter = Builders<Reservation>.Filter.Eq(g => g.Id, id);

                // Delete the document matching the filter
                await _reservationCollection.DeleteOneAsync(filter);
            }
            catch (FormatException ex)
            {
                // Handle the case where id is not a valid ObjectId
                // You can log this exception or handle it as needed
                throw new ArgumentException("Invalid ID format", ex);
            }
        }

        public async Task UpdateReservationAsync(ObjectId id, Reservation reservationdetails)
        {


            // Create a filter for the ObjectId
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, id);

            // Create an update definition for the fields you want to update
            var update = Builders<Reservation>.Update
                .Set(r => r.GuestEmail, reservationdetails.GuestEmail)
                .Set(r => r.RoomNumber, reservationdetails.RoomNumber)
                .Set(r => r.CheckInDate, reservationdetails.CheckInDate)
                .Set(r => r.CheckOutDate, reservationdetails.CheckOutDate)
                .Set(r => r.PaymentMethod, reservationdetails.PaymentMethod)
                .Set(r => r.TotalAmount, reservationdetails.TotalAmount);

            // Execute the update operation
            await _reservationCollection.UpdateOneAsync(filter, update);
        }

      

       
    }
}
