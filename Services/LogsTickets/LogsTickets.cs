using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Interfaces;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess;

namespace WEB_API_TICKETS_SUPPORT.Services.LogsTickets
{
    public class LogsTickets : ILogsTickets
    {
        internal DatabaseConnection connection = new DatabaseConnection();

        private readonly IMongoCollection<LogTicketModel> CollectionLogsTickets;

        public LogsTickets()
        {
            CollectionLogsTickets = connection.database.GetCollection<LogTicketModel>("TicketsHistoryLog");
        }

        public async Task CreateLogTicket(LogTicketModel newLogTicket)
        {
            await CollectionLogsTickets.InsertOneAsync(newLogTicket);
        }

        public async Task<List<LogTicketModel>> GetCurrentTicketsLogs()
        {
            return await CollectionLogsTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
