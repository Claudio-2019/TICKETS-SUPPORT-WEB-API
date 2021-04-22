using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_TICKETS_SUPPORT.Models
{
    public class UserAdminRegister
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string LastName { get; set; }
        [BsonElement]
        public string Phone { get; set; }
        [BsonElement]
        public string Email { get; set; }
        [BsonElement]
        public string Pass { get; set; }
        [BsonElement]
        public string Role { get; set; }
    }
}
