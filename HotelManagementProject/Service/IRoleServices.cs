using HotelManagementProject.Models;
using MongoDB.Bson;

namespace HotelManagementProject.Service
{
    public interface IRoleServices
    {
        Task<List<Roles>> GetRoleAsync();
        Task CreateRoleAsync(Roles role);
        Task UpdateRoleAsync(ObjectId id, Roles roledetails);
        Task<Roles> GetRoleByIdAsync(ObjectId id);
        Task DeleteRoleAsync(ObjectId id);
    }
}
