using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess
{
    public class DatabaseConnection
    {
        public MongoClient client;
        public IMongoDatabase database;

        public DatabaseConnection()
        {
         
            client = new MongoClient("mongodb+srv://cgonzalez:1LuK3yjbLvuPniky@ticketssupportcloud.lzc8p.mongodb.net/TicketsSupportCloud?retryWrites=true&w=majority");
            database = client.GetDatabase("TicketsSupport");
         
        }
    }
}
