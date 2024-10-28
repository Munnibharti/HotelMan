using HotelManagementProject.Models;
using MongoDB.Bson;

namespace HotelManagementProject.Service
{
    public interface IRoomService
    {
        Task<List<Room>> GetRoomAsync();
        Task CreateRoomAsync(Room room);
        Task UpdateRoomAsync(ObjectId id, Room roomdetails);
        Task<Room> GetRoomByIdAsync(ObjectId id);
        Task DeleteRoomAsync(ObjectId id);
      
    }
}
