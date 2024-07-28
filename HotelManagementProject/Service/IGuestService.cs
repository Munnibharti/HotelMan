using HotelManagementProject.Models;

namespace HotelManagementProject.Service
{
    public interface IGuestServices
    {
        Task<List<Guest>> GetGuestsAsync();
        Task CreateGuestAsync(Guest guest);
        Task UpdateGuestAsync(string id,Guest guestdetails);
        Task<Guest>  GetGuestByIdAsync(string id);
        Task DeleteGuestAsync(string id);
    }
}
