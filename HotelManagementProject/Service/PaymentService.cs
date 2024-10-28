using HotelManagementProject.Models;
using MongoDB.Driver;

namespace HotelManagementProject.Service
{
    public class PaymentService : IPaymentService
    {

        private readonly IMongoCollection<Payment> _paymentCollection;
        public PaymentService(IMongoDatabase database)
        {
            _paymentCollection = database.GetCollection<Payment>("Payments");
        }
        public async Task CreatePaymentAsync(Payment payment)
        {
            await _paymentCollection.InsertOneAsync(payment);
        }

        public async Task DeletePaymentAsync(string id)
        {
            var filter = Builders<Payment>.Filter.Eq(g => g.Id, id);
            await _paymentCollection.DeleteOneAsync(filter);
        }

        public async Task<Payment> GetPaymentByIdAsync(string id)
        {
            return await _paymentCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

           public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await _paymentCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdatePaymentAsync(string id, Payment paymentdetails)
        {
            var filter = Builders<Payment>.Filter.Eq(g => g.Id, id);
            var update = Builders<Payment>.Update
                .Set(g => g.PaymentMethod, paymentdetails.PaymentMethod);
                

           await _paymentCollection.UpdateOneAsync(filter, update);
        }
    }
}
