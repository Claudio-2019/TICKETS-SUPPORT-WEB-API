using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Interfaces;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess;

namespace WEB_API_TICKETS_SUPPORT.Services.SystemRegistrations
{
    public class ServiceRegistrations : ICurrentRegistrations
    {


        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<CurrentRegistrationModel> CollectionRegistrations;
        private readonly IMongoCollection<CurrentRegistrationModel> CollectionUsers;


        public ServiceRegistrations()
        {
            CollectionRegistrations = accessDB.database.GetCollection<CurrentRegistrationModel>("SystemRegistrations");

            CollectionUsers = accessDB.database.GetCollection<CurrentRegistrationModel>("CurrentUsers");

        }

        public async Task<List<CurrentRegistrationModel>> GetCurrentRegistrations()
        {
            return await CollectionRegistrations.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task ApproveUser(CurrentRegistrationModel newUser)
        {
            await CollectionUsers.InsertOneAsync(newUser);
        }

        public Task RejectUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
