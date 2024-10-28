using HotelManagementProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace HotelManagementProject.Service
{
    public class RoleService : IRoleServices
    {
        private readonly IMongoCollection<Roles> _roleCollection;
        public RoleService(IMongoDatabase database)
        {
            _roleCollection = database.GetCollection<Roles>("Roles");
        }
        public async Task CreateRoleAsync(Roles role)
        {
            await _roleCollection.InsertOneAsync(role);
        }

        public async Task DeleteRoleAsync(ObjectId id)
        {
            var filter = Builders<Roles>.Filter.Eq(g => g.Id,id);
            await _roleCollection.DeleteOneAsync(filter);       
        }

        public async Task<List<Roles>> GetRoleAsync()
        {
            return await _roleCollection.Find(_=>true).ToListAsync();
        }

        public async Task<Roles> GetRoleByIdAsync(ObjectId id)
        {
            return await _roleCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateRoleAsync(ObjectId id, Roles roledetails)
        {
            var filter = Builders<Roles>.Filter.Eq(g => g.Id, id);
            var update = Builders<Roles>.Update
                .Set(g => g.userName, roledetails.userName)
                .Set(g => g.Password, roledetails.Password)
                .Set(g => g.RoleType, roledetails.RoleType);
                

             await _roleCollection.UpdateOneAsync(filter, update);
        }
    }
}
