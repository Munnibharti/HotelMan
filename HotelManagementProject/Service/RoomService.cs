using HotelManagementProject.Models;
using MongoDB.Bson;
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

        public async Task DeleteRoomAsync(ObjectId id)
        {
            var filter = Builders<Room>.Filter.Eq(g => g.Id, id);
            await _roomCollection.DeleteOneAsync(filter);
        }

        public async Task<List<Room>> GetRoomAsync()
        {
            return await _roomCollection.Find(_=>true).ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(ObjectId id)
        { 
        // return await _reservationCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
            return await _roomCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
            
        }

        public async Task UpdateRoomAsync(ObjectId id, Room roomdetails)
        {
            var filter = Builders<Room>.Filter.Eq(g => g.Id, id);
            var update = Builders<Room>.Update
                .Set(g => g.Room_Number, roomdetails.Room_Number)
                .Set(g => g.Room_Type, roomdetails.Room_Type)
                
                .Set(g => g.Room_Status, roomdetails.Room_Status);

            await _roomCollection.UpdateOneAsync(filter, update);
        }
    }
}
