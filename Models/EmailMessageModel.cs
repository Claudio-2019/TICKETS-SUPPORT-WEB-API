using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_TICKETS_SUPPORT.Models
{
    public class EmailMessageModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement]
        public string Subject { get; set; }
        [BsonElement]
        public string EmailAddress { get; set; }
        [BsonElement]
        public string Message { get; set; }
        [BsonElement]
        public DateTime DateSended { get; set; }
    }
}
