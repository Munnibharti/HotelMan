using HotelManagementProject.Models;
using MongoDB.Bson;

namespace HotelManagementProject.Service
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffsAsync();
        Task CreateStaffAsync(Staff Staff);
        Task UpdateStaffAsync(ObjectId id, Staff Staffdetails);
        Task<Staff> GetStaffByIdAsync(ObjectId id);
        Task DeleteStaffAsync(ObjectId id);
    }
}
