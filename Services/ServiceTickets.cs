using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Interfaces;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess;

namespace WEB_API_TICKETS_SUPPORT.Services
{
    public class ServiceTickets : ITickets
    {

        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<TicketRequestModel> CollectionTickets;

        public ServiceTickets()
        {
            CollectionTickets = accessDB.database.GetCollection<TicketRequestModel>("CurrentTickets");
        }

        public async Task CreateTicket(TicketRequestModel newTickets)
        {
            await CollectionTickets.InsertOneAsync(newTickets);
        }

        public async Task DeleteTicket(string id)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq(X => X._id, id);

            await CollectionTickets.DeleteOneAsync(FiltroConsulta);
        }

        public async Task<List<TicketRequestModel>> GetCurrentTickets()
        {
            return await CollectionTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task UpdateTicket(string id, TicketRequestModel updateTicket)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq(X => X._id, updateTicket._id);

            await CollectionTickets.ReplaceOneAsync(FiltroConsulta, updateTicket);
        }

        public async Task<List<TicketRequestModel>> GetUserProfileTickets(string nameClient)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq("Name", nameClient);

            return await CollectionTickets.FindAsync(FiltroConsulta).Result.ToListAsync();
        }
    }
}
