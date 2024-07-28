using HotelManagementProject.Models;

namespace HotelManagementProject.Service
{
    public interface IStaffService
    {
        Task<List<Staff>> GetStaffsAsync();
        Task CreateStaffAsync(Staff Staff);
        Task UpdateStaffAsync(string id, Staff Staffdetails);
        Task<Staff> GetStaffByIdAsync(string id);
        Task DeleteStaffAsync(string id);
    }
}
