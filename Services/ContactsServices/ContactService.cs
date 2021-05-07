using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Interfaces;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess;

namespace WEB_API_TICKETS_SUPPORT.Services.ContactsServices
{
    public class ContactService : IContacts
    {
        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<ContactModel> CollectionContacts;
        public ContactService()
        {
            CollectionContacts = accessDB.database.GetCollection<ContactModel>("Contacts");
        }
        public async Task AddContact(ContactModel contactInfo)
        {
             await CollectionContacts.InsertOneAsync(contactInfo);
        }

        public async Task<List<ContactModel>> GetContacts()
        {
            return await CollectionContacts.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
