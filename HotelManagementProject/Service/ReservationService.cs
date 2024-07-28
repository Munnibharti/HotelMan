using HotelManagementProject.Models;
using MongoDB.Driver;

namespace HotelManagementProject.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        public ReservationService(IMongoDatabase database)
        {
            _reservationCollection = database.GetCollection<Reservation>("Reservations");
        }
        public async Task<List<Reservation>> GetReservationAsync()
        {
            return await _reservationCollection.Find(_ => true).ToListAsync();
        }
        public async Task CreatereservationAsync(Reservation reservation)
        {
            await _reservationCollection.InsertOneAsync(reservation);
        }
       

        public async Task<Reservation> GetReservationByIdAsync(string id)
        {
            return await _reservationCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }


        public async Task DeleteReservationAsync(string id)
        {
            var filter = Builders<Reservation>.Filter.Eq(g => g.Id, id);
            await _reservationCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateReservationAsync(string id, Reservation reservationdetails)
        {
            var filter = Builders<Reservation>.Filter.Eq(g => g.Id, id);
            var update = Builders<Reservation>.Update
                .Set(g => g.GuestId, reservationdetails.GuestId)
                .Set(g => g.RoomId, reservationdetails.RoomId)
                .Set(g => g.CheckInDate, reservationdetails.CheckInDate)
                .Set(g => g.CheckOutDate, reservationdetails.CheckOutDate)
                .Set(g => g.PaymentId, reservationdetails.PaymentId)
                .Set(g => g.TotalAmount, reservationdetails.TotalAmount);

            await _reservationCollection.UpdateOneAsync(filter, update);
        }

      

       
    }
}
