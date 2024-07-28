using HotelManagementProject.Models;
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

        public async Task DeleteStaffAsync(string id)
        {
            var filter = Builders<Staff>.Filter.Eq(g => g.Id, id);
            await _staffCollection.DeleteOneAsync(filter);
        }

        public async Task<Staff> GetStaffByIdAsync(string id)
        {
            return await _staffCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Staff>> GetStaffsAsync()
        {
            return await _staffCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateStaffAsync(string id, Staff Staffdetails)
        {
            var filter = Builders<Staff>.Filter.Eq(g => g.Id, id);
            var update = Builders<Staff>.Update
                .Set(g => g.Staff_Name, Staffdetails.Staff_Name)
                .Set(g => g.Position, Staffdetails.Position)
                .Set(g => g.salary, Staffdetails.salary);
              

            await _staffCollection.UpdateOneAsync(filter, update);
        }
    }
}
