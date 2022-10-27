using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Models
{
    public class FuelStation
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string StationName { get; set; }
        public string Address { get; set; }
    }
}
