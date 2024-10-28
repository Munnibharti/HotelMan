using HotelManagementProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotelManagementProject.Service
{
    public class StaffService : IStaffService
    {
        private readonly IMongoCollection<Staff> _staffCollection;
        public StaffService(IMongoDatabase database)
        {
            _staffCollection = database.GetCollection<Staff>("Staffs");
        }
        public async Task CreateStaffAsync(Staff Staff)
        {
            await _staffCollection.InsertOneAsync(Staff);
        }

        public async Task DeleteStaffAsync(ObjectId id)
        {
            var filter = Builders<Staff>.Filter.Eq(g => g.Id, id);
            await _staffCollection.DeleteOneAsync(filter);
        }

        public async Task<Staff> GetStaffByIdAsync(ObjectId id)
        {
            return await _staffCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Staff>> GetStaffsAsync()
        {
            return await _staffCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateStaffAsync(ObjectId id, Staff staffdetails)
        {
            var filter = Builders<Staff>.Filter.Eq(g => g.Id, id);
            var update = Builders<Staff>.Update
                .Set(g => g.user_Name, staffdetails.user_Name)
                .Set(g => g.Position, staffdetails.Position)
                .Set(g => g.salary, staffdetails.salary);
              

            await _staffCollection.UpdateOneAsync(filter, update);
        }
    }
}
