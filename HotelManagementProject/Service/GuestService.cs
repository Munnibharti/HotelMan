using HotelManagementProject.Models;
using MongoDB.Driver;

namespace HotelManagementProject.Service
{
    public class GuestService : IGuestServices
    {
        private readonly IMongoCollection<Guest> _guestCollection;
        public GuestService(IMongoDatabase database)
        {
            _guestCollection = database.GetCollection<Guest>("Guests");
        }
        public async Task<List<Guest>> GetGuestsAsync()
        {
            return await _guestCollection.Find(_ => true).ToListAsync();
        }
        public async Task CreateGuestAsync(Guest guest)
        {
            await _guestCollection.InsertOneAsync(guest);
        }
        public async Task UpdateGuestAsync(string id, Guest guestDetails)
        {
            var filter = Builders<Guest>.Filter.Eq(g => g.Id, id);
            var update = Builders<Guest>.Update
                .Set(g => g.Guest_Name, guestDetails.Guest_Name)
                .Set(g => g.Guest_Email, guestDetails.Guest_Email)
                .Set(g => g.Guest_Address, guestDetails.Guest_Address)
                .Set(g => g.Guest_Phone, guestDetails.Guest_Phone);

            await _guestCollection.UpdateOneAsync(filter, update);
        }

        public async Task<Guest>  GetGuestByIdAsync(string id)
        {
            return await _guestCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }
       

        public async Task DeleteGuestAsync(string id)
        {
            var filter = Builders<Guest>.Filter.Eq(g => g.Id, id);
            await _guestCollection.DeleteOneAsync(filter);
        }

    }
    
}
