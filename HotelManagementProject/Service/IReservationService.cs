using HotelManagementProject.Models;
using MongoDB.Bson;

namespace HotelManagementProject.Service
{
    public interface IReservationService
    {
       Task<List<Reservation>> GetReservationAsync();
        Task CreatereservationAsync(Reservation reservation);
        //Task<List<Reservation>> GetReservationsWithDetailsAsync();
        Task UpdateReservationAsync(ObjectId id, Reservation reservationdetails);
        Task<Reservation> GetReservationByIdAsync(ObjectId id);
        Task DeleteReservationAsync(ObjectId id);
    }
}
