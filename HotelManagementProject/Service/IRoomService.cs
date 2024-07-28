using HotelManagementProject.Models;

namespace HotelManagementProject.Service
{
    public interface IRoomService
    {
        Task<List<Room>> GetRoomAsync();
        Task CreateRoomAsync(Room room);
        Task UpdateRoomAsync(string id, Room roomdetails);
        Task<Room> GetRoomByIdAsync(string id);
        Task DeleteRoomAsync(string id);
    }
}
