using HotelManagementProject.Models;
using MongoDB.Driver;

namespace HotelManagementProject.Service
{
    public class RoomService : IRoomService
    {

        private readonly IMongoCollection<Room> _roomCollection;
        public RoomService(IMongoDatabase database)
        {
            _roomCollection = database.GetCollection<Room>("Rooms");
        }
        public async  Task CreateRoomAsync(Room room)
        {
            await _roomCollection.InsertOneAsync(room);
        }

        public async Task DeleteRoomAsync(string id)
        {
            var filter = Builders<Room>.Filter.Eq(g => g.Id, id);
            await _roomCollection.DeleteOneAsync(filter);
        }

        public async Task<List<Room>> GetRoomAsync()
        {
            return await _roomCollection.Find(_=>true).ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(string id)
        {
            return await _roomCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
            
        }

        public async Task UpdateRoomAsync(string id, Room roomdetails)
        {
            var filter = Builders<Room>.Filter.Eq(g => g.Id, id);
            var update = Builders<Room>.Update
                .Set(g => g.Room_Number, roomdetails.Room_Number)
                .Set(g => g.Room_Type, roomdetails.Room_Type)
                .Set(g => g.Room_Price, roomdetails.Room_Price)
                .Set(g => g.Room_Status, roomdetails.Room_Status);

            await _roomCollection.UpdateOneAsync(filter, update);
        }
    }
}
