using HotelManagementProject.Models;

namespace HotelManagementProject.Service
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetReservationAsync();
        Task CreatereservationAsync(Reservation reservation);

        Task UpdateReservationAsync(string id, Reservation reservationdetails);
        Task<Reservation> GetReservationByIdAsync(string id);
        Task DeleteReservationAsync(string id);
    }
}
