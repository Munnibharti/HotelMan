using HotelManagementProject.Models;

namespace HotelManagementProject.Service
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetPaymentsAsync();
        Task CreatePaymentAsync(Payment payment);
        Task UpdatePaymentAsync(string id, Payment paymentdetails);
        Task<Payment> GetPaymentByIdAsync(string id);
        Task DeletePaymentAsync(string id);
    }
}
