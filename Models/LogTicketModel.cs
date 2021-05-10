using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_TICKETS_SUPPORT.Models
{
    public class LogTicketModel
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public int TicketNumber { get; set; }
        [BsonElement]
        public string TypeRequest { get; set; }
        [BsonElement]
        public string Details { get; set; }
        [BsonElement]
        public string SolutionDetails { get; set; }
    }
}
