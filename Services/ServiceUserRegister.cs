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
    public class ServiceUserRegister : IUserRegister
    {

        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<UserRegisterModel> CollectionUsers;

        private readonly IMongoCollection<UserAdminRegisterModel> CollectionAdministrators;

        public ServiceUserRegister()
        {
            CollectionUsers = accessDB.database.GetCollection<UserRegisterModel>("CurrentUsers");

            CollectionAdministrators = accessDB.database.GetCollection<UserAdminRegisterModel>("CurrentAdministrators");
        }

        public async Task CreateUserAccount(UserRegisterModel UserData)
        {
            await CollectionUsers.InsertOneAsync(UserData);
        }
        public async Task CreateAdminAccount(UserAdminRegisterModel AdminData)
        {
            await CollectionAdministrators.InsertOneAsync(AdminData);
        }
        public async Task<List<UserRegisterModel>> GetCurrentUsers()
        {
            return await CollectionUsers.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<List<UserAdminRegisterModel>> GetCurrentAdmin()
        {
            return await CollectionAdministrators.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task DeleteUser(string _id)
        {
            var FiltroConsulta = Builders<UserRegisterModel>.Filter.Eq(X => X._id, _id);

            await CollectionUsers.DeleteOneAsync(FiltroConsulta);
        }
        public async Task UpdateUser(UserRegisterModel update)
        {
            var FiltroConsulta = Builders<UserRegisterModel>.Filter.Eq(X => X._id, update._id);

            await CollectionUsers.ReplaceOneAsync(FiltroConsulta, update);
        }
        //Search for Session User
        public async Task<List<UserRegisterModel>> GetCurrentSessionUser(string email)
        {
            var FiltroConsulta = Builders<UserRegisterModel>.Filter.Eq("Email",email);

            return await CollectionUsers.FindAsync(FiltroConsulta).Result.ToListAsync();
        }
        public async Task<List<UserAdminRegisterModel>> GetCurrentSessionAdministrator(string email)
        {
            var FiltroConsulta = Builders<UserAdminRegisterModel>.Filter.Eq("Email", email);

            return await CollectionAdministrators.FindAsync(FiltroConsulta).Result.ToListAsync();
        }





    }
}
