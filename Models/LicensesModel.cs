using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_TICKETS_SUPPORT.Models
{
    public class LicensesModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement]
        public string LicenseName { get; set; }
        [BsonElement]
        public string Category { get; set; }
        [BsonElement]
        public DateTime ExpirationDate { get; set; }
        [BsonElement]
        public string Description { get; set; }
        [BsonElement]
        public string Company { get; set; }
    }
}
