using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Models
{
    public class Review
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid ReviewerId { get; set; }
        public Guid StationId { get; set; }
        public string Comment { get; set; }
        public int StarRate { get; set; }
    }
}
