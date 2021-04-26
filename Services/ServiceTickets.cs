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

        private readonly IMongoCollection<TicketRequest> CollectionTickets;

        public ServiceTickets()
        {
            CollectionTickets = accessDB.database.GetCollection<TicketRequest>("CurrentTickets");
        }

        public async Task CreateTicket(TicketRequest newTickets)
        {
            await CollectionTickets.InsertOneAsync(newTickets);
        }

        public async Task DeleteTicket(string id)
        {
            var FiltroConsulta = Builders<TicketRequest>.Filter.Eq(X => X._id, id);

            await CollectionTickets.DeleteOneAsync(FiltroConsulta);
        }

        public async Task<List<TicketRequest>> GetCurrentTickets()
        {
            return await CollectionTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task UpdateTicket(TicketRequest updateTicket)
        {
            var FiltroConsulta = Builders<TicketRequest>.Filter.Eq(X => X._id, updateTicket._id);

            await CollectionTickets.ReplaceOneAsync(FiltroConsulta, updateTicket);
        }
    }
}
