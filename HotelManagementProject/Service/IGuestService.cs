using HotelManagementProject.Models;
using MongoDB.Bson;

namespace HotelManagementProject.Service
{
    public interface IGuestServices
    {
        Task<List<Guest>> GetGuestsAsync();
        Task CreateGuestAsync(Guest guest);
        Task UpdateGuestAsync(ObjectId id,Guest guestdetails);
        Task<Guest>  GetGuestByIdAsync(ObjectId id);
        Task DeleteGuestAsync(ObjectId id);
    }
}
